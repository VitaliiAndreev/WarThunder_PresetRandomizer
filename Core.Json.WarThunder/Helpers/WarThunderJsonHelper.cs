using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Enumerations.Logger;
using Core.Json.Exceptions;
using Core.Json.Extensions;
using Core.Json.WarThunder.Enumerations.Logger;
using Core.Json.WarThunder.Extensions;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Json.WarThunder.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.Helpers
{
    /// <summary> Provides methods to work with JSON data specific to War Thunder. </summary>
    public class WarThunderJsonHelper : JsonHelper, IWarThunderJsonHelper
    {
        #region Constants

        /// <summary> The name of the JSON property containing a research tree column. </summary>
        private const string _researchTreeColumnJsonPropertyName = "range";

        /// <summary> The name of the JSON property containing a vehicle's rank in the research tree. </summary>
        private const string _rankJsonPropertyName = "rank";

        /// <summary> The name of the JSON property containing a vehicle folder's image Gaijin ID. </summary>
        private const string _folderImageJsonPropertyName = "image";

        /// <summary> The name of the JSON property containing the name of the vehicle required to unlock the current one. </summary>
        private const string _requiredVehiclePropertyName = "reqAir";

        #endregion Constants
        #region Constructors

        /// <summary> Creates a new War Thunder JSON helper. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public WarThunderJsonHelper(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
        }

        #endregion Constructors
        #region Methods: [Private] GetPotentiallyDuplicatePropertyNames()

        /// <summary> Looks through the specified JSON container and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="jsonContainer"> The JSON container to search in. </param>
        /// <returns></returns>
        private ISet<string> GetPotentiallyDuplicatePropertyNamesInContainer(JContainer jsonContainer)
        {
            var duplicatePropertyNames = new HashSet<string>();

            if (jsonContainer is JArray jsonArray)
            {
                var propertyNames = jsonArray
                    .Children()
                    .OfType<JObject>()
                    .SelectMany(fragmentedJsonObject => fragmentedJsonObject.Properties().Select(jsonProperty => jsonProperty.Name))
                ;

                duplicatePropertyNames.AddRange(propertyNames.Where(propertyName => propertyNames.Count(item => item == propertyName) > 1));
            }

            foreach (var jsonToken in jsonContainer)
            {
                if (jsonToken is JContainer childJsonContainer)
                    duplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNamesInContainer(childJsonContainer));
            }

            return duplicatePropertyNames;
        }

        /// <summary> Looks through the specified JSON token and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="jsonToken"> The JSON token to search in. </param>
        /// <returns></returns>
        private ISet<string> GetPotentiallyDuplicatePropertyNames(JToken jsonToken)
        {
            var potentiallyDuplicatePropertyNames = new HashSet<string>();

            if (jsonToken is JContainer jsonContainer)
                potentiallyDuplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNamesInContainer(jsonContainer));

            return potentiallyDuplicatePropertyNames;
        }

        /// <summary> Looks through the specified dictionary of entities and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="entities"> The dictionary of entities to search in. </param>
        /// <returns></returns>
        private ISet<string> GetPotentiallyDuplicatePropertyNames(IDictionary<string, dynamic> entities)
        {
            var potentiallyDuplicatePropertyNames = new HashSet<string>();

            foreach (var keyValuePair in entities)
                potentiallyDuplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNames(keyValuePair.Value) as ISet<string>);

            return potentiallyDuplicatePropertyNames;
        }

        #endregion Methods: [Private] GetPotentiallyDuplicatePropertyNames()
        #region Methods: [Protected] Deserialization with Standardization

        /// <summary> Handles potentially duplicate property names in the specified JSON object. </summary>
        /// <param name="jsonObject"> The JSON object to process. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject StandardizeObject(JObject jsonObject, IEnumerable<string> duplicatePropertyNames)
        {
            foreach (var jsonPropertyName in jsonObject.GetPropertyNames())
            {
                var jsonToken = jsonObject[jsonPropertyName];

                if (jsonPropertyName.IsIn(duplicatePropertyNames))
                    jsonToken.ConvertIntoArray();

                if (jsonToken is JContainer jsonContainer)
                    jsonObject[jsonPropertyName] = StandardizeContainer(jsonContainer, duplicatePropertyNames);
            }
            return jsonObject;
        }

        /// <summary> Flattens a JSON array of objects into a single JSON object. </summary>
        /// <param name="jsonArray"> The JSON array to flatten. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject ReconstructObjectFromArray(JArray jsonArray, IEnumerable<string> duplicatePropertyNames)
        {
            var rebuiltJsonObject = new JObject();

            foreach (var fragmentedJsonToken in jsonArray)
            {
                if (fragmentedJsonToken is JObject fragmentedJsonObject)
                {
                    foreach (var jsonProperty in fragmentedJsonObject)
                    {
                        if (jsonProperty.Key.IsIn(duplicatePropertyNames))
                        {
                            if (rebuiltJsonObject.Properties().Select(property => property.Name).Contains(jsonProperty.Key))
                            {
                                if (rebuiltJsonObject[jsonProperty.Key] is JArray existingJsonArray)
                                    existingJsonArray.Add(jsonProperty.Value is JArray ? jsonProperty.Value.First() : jsonProperty.Value);
                                else
                                    throw new NotImplementedException();
                            }
                            else
                                rebuiltJsonObject.Add(new JProperty(jsonProperty.Key, jsonProperty.Value is JArray ? jsonProperty.Value : new JArray(jsonProperty.Value)));
                        }
                        else
                        {
                            rebuiltJsonObject.Add(new JProperty(jsonProperty.Key, jsonProperty.Value));
                        }
                    }
                }
                else
                    throw new NotImplementedException();
            }

            return rebuiltJsonObject;
        }

        /// <summary> Handles potentially duplicate property names in the specified JSON array. </summary>
        /// <param name="jsonArray"> The JSON array to process. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JContainer StandardizeArray(JArray jsonArray, IEnumerable<string> duplicatePropertyNames)
        {
            for (var i = 0; i < jsonArray.Count(); i++)
            {
                var jsonToken = jsonArray[i];

                if (jsonToken is JContainer jsonContainer)
                    jsonArray[i] = StandardizeContainer(jsonContainer, duplicatePropertyNames);
            }

            return jsonArray.HasPotentiallyDuplicateProperties()
                ? ReconstructObjectFromArray(jsonArray, duplicatePropertyNames)
                : jsonArray as JContainer;
        }

        /// <summary>
        /// Standardizes the specified JSON container into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonContainer"> The JSON container to standardize. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JContainer StandardizeContainer(JContainer jsonContainer, IEnumerable<string> duplicatePropertyNames)
        {
            if (jsonContainer is JObject jsonObject)
                return StandardizeObject(jsonObject, duplicatePropertyNames);
            else if (jsonContainer is JArray jsonArray)
                return StandardizeArray(jsonArray, duplicatePropertyNames);
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Standardizes the specified JSON token into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonToken"> The JSON token to standardize. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject StandardizeTokenIntoObject(JToken jsonToken, IEnumerable<string> duplicatePropertyNames)
        {
            if (jsonToken is JContainer container)
                return StandardizeContainer(container, duplicatePropertyNames) as JObject;
            else
                throw new JsonStandardizationException(EJsonLogMessage.MustBeJsonContainerToStandardize);
        }

        /// <summary>
        /// Deserializes and standardizes JSON text into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        protected override JObject StandardizeAndDeserializeObject(string jsonText)
        {
            var entity = DeserializeObject<dynamic>(jsonText);

            return StandardizeTokenIntoObject(entity, GetPotentiallyDuplicatePropertyNames(entity));
        }

        /// <summary>
        /// Deserializes and standardizes JSON text into a dictionary of JSON objects.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        protected override IDictionary<string, JObject> StandardizeAndDeserializeObjects(string jsonText)
        {
            var entities = DeserializeDictionary<dynamic>(jsonText);
            var standardizedJsonObjects = new Dictionary<string, JObject>();
            var duplicatePropertyNames = GetPotentiallyDuplicatePropertyNames(entities);

            foreach (var keyValuePair in entities)
                standardizedJsonObjects.Add(keyValuePair.Key, StandardizeTokenIntoObject(keyValuePair.Value, duplicatePropertyNames));

            return standardizedJsonObjects;
        }

        #endregion [Protected] Methods: Deserialization with Standardization
        #region Methods: [Private] Deserialization of Research Trees

        /// <summary> Deserializes the given JSON object into instances of transient objects representing in-game research tree branches the way they are stored in JSON files. </summary>
        /// <param name="researchTreeAsJsonObject"> The JSON object to deserialize. </param>
        /// <returns></returns>
        private IEnumerable<ResearchTreeBranchFromJson> DeserializeResearchTreeBranches(JObject researchTreeAsJsonObject)
        {
            var branches = new List<ResearchTreeBranchFromJson>();
            
            foreach (var jsonProperty in researchTreeAsJsonObject.Properties())
            {
                var branch = new ResearchTreeBranchFromJson(jsonProperty.Name);

                if (jsonProperty.Value is JArray jsonArray)
                    branch.Columns.AddRange(DeserializeResearchTreeColumns(jsonArray));
                else if (jsonProperty.Value is JObject jsonObject)
                    branch.Columns.Add(DeserializeResearchTreeColumn(jsonObject, EInteger.Number.One));
                else
                    throw new JsonDeserializationException(EJsonWarThunderLogMessage.BranchIsEmpty.FormatFluently(branch.GaijinId));

                branches.Add(branch);
            }
            return branches;
        }

        /// <summary> Checks whether the given JSON token contains a research tree column. </summary>
        /// <param name="jsonToken"> The JSON token to check. </param>
        /// <param name="outputJsonObject"> The JSON object containing a research tree column. </param>
        /// <returns></returns>
        private bool JsonTokenContainsResearchTreeColumnAsJsonObject(JToken jsonToken, out JObject outputJsonObject)
        {
            if (jsonToken is JObject jsonObject && jsonObject.Children().FirstOrDefault() is JProperty jsonProperty && jsonProperty.Name == _researchTreeColumnJsonPropertyName && jsonProperty.Value is JObject researchTreeColumnAsJsonObject)
            {
                outputJsonObject = researchTreeColumnAsJsonObject;
                return true;
            }
            outputJsonObject = null;
            return false;
        }

        /// <summary> Deserializes the given JSON array into instances of transient objects representing in-game research tree branch columns the way they are stored in JSON files. </summary>
        /// <param name="jsonArrayOfResearchTreeColumns"> The JSON array to deserialize. </param>
        /// <returns></returns>
        private IEnumerable<ResearchTreeColumnFromJson> DeserializeResearchTreeColumns(JArray jsonArrayOfResearchTreeColumns)
        {
            var columns = new List<ResearchTreeColumnFromJson>();

            foreach (var jsonToken in jsonArrayOfResearchTreeColumns)
            {
                var columnIndex = columns.Count() + EInteger.Number.One;
                var column = DeserializeResearchTreeColumn(jsonToken, columnIndex);
                columns.Add(column);
            }
            return columns;
        }

        /// <summary> Deserializes the given JSON token into an instance of a transient object representing an in-game research tree branch column the way it is stored in JSON files. </summary>
        /// <param name="jsonToken"> The JSON token to deserialize. </param>
        /// <param name="columnIndex"> The index of the column (the 1-based X coordinate). </param>
        /// <returns></returns>
        private ResearchTreeColumnFromJson DeserializeResearchTreeColumn(JToken jsonToken, int columnIndex)
        {
            var column = new ResearchTreeColumnFromJson();

            if (JsonTokenContainsResearchTreeColumnAsJsonObject(jsonToken, out var researchTreeColumnAsJsonObject))
                column.Cells.AddRange(DeserializeResearchTreeCells(researchTreeColumnAsJsonObject, columnIndex));
            else
                throw new JsonDeserializationException(EJsonWarThunderLogMessage.ColumnIsEmpty);

            return column;
        }

        /// <summary> Checks whether the given JSON token contains a research tree vehicle folder. </summary>
        /// <param name="researchTreeCellAsJsonObject"> The JSON object to check. </param>
        /// <returns></returns>
        private bool ResearchTreeCellIsFolder(JObject researchTreeCellAsJsonObject) =>
            researchTreeCellAsJsonObject
                .Properties()
                .Any(property => property.Name == _folderImageJsonPropertyName);

        /// <summary> Deserializes the given JSON object into instances of transient objects representing in-game research tree cells the way they are stored in JSON files. </summary>
        /// <param name="researchTreeColumnAsJsonObject"> The JSON object to deserialize. </param>
        /// <param name="columnIndex"> The index of the parent column (the 1-based X coordinate). </param>
        /// <returns></returns>
        private IEnumerable<ResearchTreeCellFromJson> DeserializeResearchTreeCells(JObject researchTreeColumnAsJsonObject, int columnIndex)
        {
            var cells = new List<ResearchTreeCellFromJson>();

            foreach (var jsonProperty in researchTreeColumnAsJsonObject.Properties())
            {
                var researchTreeCellAsJsonObject = StandardizeAndDeserializeObject(jsonProperty.Value.ToString());
                var cell = default(ResearchTreeCellFromJson);

                jsonProperty.Value = researchTreeCellAsJsonObject;

                if (ResearchTreeCellIsFolder(researchTreeCellAsJsonObject))
                    cell = DeserializeResearchTreeCellFolder(researchTreeCellAsJsonObject, columnIndex);
                else
                    cell = new ResearchTreeCellVehicleFromJson(DeserializeResearchTreeVehicle(jsonProperty, columnIndex));

                cell.SetRowWithinRank(cells);
                cells.Add(cell);
            }
            return cells;
        }

        /// <summary> Deserializes the given JSON object into an instance of a transient object representing an in-game research tree vehicle cell folder the way it is stored in JSON files. </summary>
        /// <param name="researchTreeFolderAsJsonObject"> The JSON object to deserialize. </param>
        /// <param name="columnIndex"> The index of the parent column (the 1-based X coordinate). </param>
        /// <returns></returns>
        private ResearchTreeCellFolderFromJson DeserializeResearchTreeCellFolder(JObject researchTreeFolderAsJsonObject, int columnIndex)
        {
            var folder = new ResearchTreeCellFolderFromJson();

            foreach (var jsonProperty in researchTreeFolderAsJsonObject.Properties())
            {
                if (jsonProperty.Value is JObject)
                    folder.Vehicles.Add(DeserializeResearchTreeVehicle(jsonProperty, columnIndex));
            }

            if (folder.Vehicles.Any())
                folder.Rank = folder.Vehicles.First().Rank;

            return folder;
        }

        /// <summary> Patches the required vehicle name property value to avoid multiple empty requirements in cases such as (H8K3). </summary>
        /// <param name="researchTreeVehicleAsJsonObject"> The JSON object to patch. </param>
        private void PatchRequiredVehicleTokens(JObject researchTreeVehicleAsJsonObject)
        {
            var requiredVehicleNameTokens = researchTreeVehicleAsJsonObject[_requiredVehiclePropertyName] is JArray requiredVehicles
                ? requiredVehicles.Where(jsonToken => jsonToken is JValue jsonValue && jsonValue.Value is string requiredVehicleName && !string.IsNullOrWhiteSpace(requiredVehicleName))
                : new List<JToken> { researchTreeVehicleAsJsonObject[_requiredVehiclePropertyName] };

            if (requiredVehicleNameTokens.IsEmpty())
                researchTreeVehicleAsJsonObject[_requiredVehiclePropertyName] = new JValue(string.Empty);

            else if (requiredVehicleNameTokens.HasSingle())
                researchTreeVehicleAsJsonObject[_requiredVehiclePropertyName] = requiredVehicleNameTokens.First() as JValue;

            else if (requiredVehicleNameTokens.HasSeveral())
                throw new NotSupportedException(EJsonWarThunderLogMessage.SeveralRequiredVehicleIsNotSupportedYet);
        }

        /// <summary> Deserializes the given JSON property into an instance of a transient object representing an in-game research tree vehicle the way it is stored in JSON files. </summary>
        /// <param name="jsonProperty"> The JSON property to deserialize. </param>
        /// <param name="columnIndex"> The index of the parent column (the 1-based X coordinate). </param>
        /// <returns></returns>
        private ResearchTreeVehicleFromJson DeserializeResearchTreeVehicle(JProperty jsonProperty, int columnIndex)
        {
            if (jsonProperty.Value is JObject researchTreeVehicleAsJsonObject)
            {
                PatchRequiredVehicleTokens(researchTreeVehicleAsJsonObject);

                var vehicle = DeserializeObject<ResearchTreeVehicleFromJson>(researchTreeVehicleAsJsonObject.ToString());
                vehicle.GaijinId = jsonProperty.Name;
                vehicle.CellCoordinatesWithinRank = vehicle.PresetCellCoordinatesWithinRank is List<int> presetCoordinates
                    ? presetCoordinates
                    : new List<int> { columnIndex };

                return vehicle;
            }
            throw new JsonDeserializationException(EJsonWarThunderLogMessage.ObjectNotRecognizedAsResearchTreeVehicle);
        }

        #endregion Methods: [Private] Deserialization of Research Trees
        #region Methods: [Public] Deserialization

        /// <summary> Deserializes given JSON text into instances of interim non-persistent objects. </summary>
        /// <typeparam name="T"> A generic type of JSON mapping classes. </typeparam>
        /// <param name="jsonText"> JSON text to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<T> DeserializeList<T>(string jsonText) where T : DeserializedFromJsonWithGaijinId =>
            DeserializeDictionary<T>(jsonText).FinalizeDeserialization().Values;

        /// <summary> Deserializes given JSON text into instances of persistent objects. </summary>
        /// <typeparam name="T"> A generic type of persistent objects. </typeparam>
        /// <param name="dataRepository"> The data repository to assign new instances to. </param>
        /// <param name="jsonText"> JSON text to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<T> DeserializeList<T>(IDataRepository dataRepository, string jsonText) where T : PersistentObjectWithIdAndGaijinId
        {
            var deserializedInstances = new List<T>();

            if (typeof(T) == typeof(Nation))
            {
                foreach (var deserializedData in DeserializeObject<RankDeserializedFromJson>(jsonText, true).Nations)
                    deserializedInstances.Add(new Nation(dataRepository, deserializedData) as T);
            }
            else if (typeof(T) == typeof(Vehicle))
            {
                foreach (var deserializedData in DeserializeList<VehicleDeserializedFromJsonWpCost>(jsonText))
                    deserializedInstances.Add(new Vehicle(dataRepository, deserializedData) as T);
            }
            return deserializedInstances;
        }

        /// <summary> Deserializes given JSON text into instances of transient objects representing in-game research trees the way they are stored in JSON files. </summary>
        /// <param name="jsonText"> JSON text to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<ResearchTreeFromJson> DeserializeResearchTrees(string jsonText)
        {
            var rootJsonObject = DeserializeObject<dynamic>(jsonText) as JObject;
            var researchTrees = new List<ResearchTreeFromJson>();

            foreach (var jsonProperty in rootJsonObject.Properties())
            {
                if (jsonProperty.Value is JObject jsonObject)
                {
                    var researchTree = new ResearchTreeFromJson(jsonProperty.Name);
                    researchTree.Branches.AddRange(DeserializeResearchTreeBranches(jsonObject));

                    researchTrees.Add(researchTree);
                }
            }
            return researchTrees;
        }

        #endregion Methods: [Public] Deserialization
    }
}