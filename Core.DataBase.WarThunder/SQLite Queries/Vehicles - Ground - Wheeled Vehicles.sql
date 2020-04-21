select
	substr(nation.GaijinId, 9) [Nation]
	,case lower(vehicle.Country)
		when lower(substr(nation.GaijinId, 9)) then ''
		else vehicle.Country
	end [Country]
	,fullName.English [Name]
	,vehicle.GaijinId [Gaijin ID]
	,vehicle.Rank [Rank]
	,vehicle.Class [Class]
	,vehicle.IsHiddenUnlessOwned [Hidden]
	,vehicle.IsPremium [Premium]
	,vehicle.IsPurchasableForGoldenEagles [GE]
	,vehicle.IsSoldInTheStore [Store]
	,vehicle.IsSoldOnTheMarket [Market]
	,vehicle.IsAvailableOnlyOnConsoles [Consoles Only]
from
	objVehicles vehicle
	join locVehicles_FullName fullName on fullName.objVehicles_Id = vehicle.Id
	join objNations nation on nation.Id = vehicle.objNations_Id
	left join objGroundVehicleTags groundTagSet on groundTagSet.objVehicles_Id = vehicle.Id
where
	groundTagSet.IsWheeled
	and vehicle.GaijinId not like '%_football'
	and vehicle.GaijinId not like '%_nw'
	and vehicle.GaijinId not like '%_race'
	and vehicle.GaijinId not like '%_space_suit'
	and vehicle.GaijinId not like '%_tutorial'
	and vehicle.GaijinId not like 'uk_centaur_aa_mk_%'
order by
	[Nation], [Country], [Class], [Name]