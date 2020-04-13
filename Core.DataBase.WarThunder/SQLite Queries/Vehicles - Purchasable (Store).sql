select
	substr(nation.GaijinId, 9) [Nation]
	,case lower(vehicle.Country)
		when lower(substr(nation.GaijinId, 9)) then ''
		else vehicle.Country
	end [Country]
	,substr(branch.GaijinId, length(substr(nation.GaijinId, 9)) + 2) [Branch]
	,fullName.English [Name]
	,vehicle.GaijinId [Gaijin ID]
	,vehicle.Rank [Rank]
	,vehicle.Class [Class]
	,case subclass.First
		when 'None' then ''
		when vehicle.Class then ''
		else subclass.First
	end [Subclass 1]
	,case subclass.Second
		when 'None' then ''
		when subclass.First then ''
		else subclass.Second
	end [Subclass 2]
	,vehicle.IsAvailableOnlyOnConsoles [Consoles Only]
from
	objVehicles vehicle
	join locVehicles_FullName fullName on fullName.objVehicles_Id = vehicle.Id
	join objBranches branch on branch.Id = vehicle.objBranches_Id
	join objNations nation on nation.Id = vehicle.objNations_Id
	join objVehicles_SubClass subclass on subclass.objVehicles_Id = vehicle.Id
where
	vehicle.IsSoldInTheStore
	and vehicle.GaijinId not like '%_football'
	and vehicle.GaijinId not like '%_nw'
	and vehicle.GaijinId not like '%_race'
	and vehicle.GaijinId not like '%_space_suit'
	and vehicle.GaijinId not like '%_tutorial'
	and vehicle.GaijinId not like 'uk_centaur_aa_mk_%'
order by
	[Nation], [Country], [Branch], [Class], [Subclass 1], [Subclass 2], [Name], [Consoles Only]