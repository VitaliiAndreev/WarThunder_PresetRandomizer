select
	nation.AsEnumerationItem [Nation]
	,vehicle.Country [Country]
	,branch.AsEnumerationItem [Branch]
	,fullName.English [Name]
	,vehicle.GaijinId [Gaijin ID]
	,vehicle.Rank [Rank]
	,vehicle.Class [Class]
	,subclass.First [Subclass 1]
	,subclass.Second [Subclass 2]
	,case economy.UnlockCostInResearch
		when 0 then null
		else economy.UnlockCostInResearch
	end [RP Cost]
	,economy.DiscountedPurchaseCostInGold [GE Cost With Maximum Discount]
from
	objVehicles vehicle
	join locVehicles_FullName fullName on fullName.objVehicles_Id = vehicle.Id
	join objBranches branch on branch.Id = vehicle.objBranches_Id
	join objNations nation on nation.Id = vehicle.objNations_Id
	join objVehicles_SubClass subclass on subclass.objVehicles_Id = vehicle.Id
	join objVehicles_EconomyData economy on economy.objVehicles_Id = vehicle.Id
where
	vehicle.IsSquadronVehicle
	and vehicle.GaijinId not like '%_football'
	and vehicle.GaijinId not like '%_nw'
	and vehicle.GaijinId not like '%_race'
	and vehicle.GaijinId not like '%_space_suit'
	and vehicle.GaijinId not like '%_tutorial'
	and vehicle.GaijinId not like 'uk_centaur_aa_mk_%'
order by
	[Nation], [Country], [Branch], [Class], [Subclass 1], [Subclass 2], [Name]