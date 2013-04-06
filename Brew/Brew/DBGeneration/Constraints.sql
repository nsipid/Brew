ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Beta_Range CHECK (
   Beta >= 0 AND Beta <= 100 --Inclusive Percent
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Alpha_Range CHECK (
   Alpha >= 0 AND Alpha <= 100 --Inclusive Percent
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Amount_Range CHECK (
   Amount >= 0 AND Amount <= 1000 --Inclusive 
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_HSI_Range CHECK (
   HSI >= 0 AND HSI <= 100 --Inclusive Percent
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Humulene_Range CHECK (
   Humulene >= 0 AND Humulene <= 100 --Inclusive Percent
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Caryophyllene_Range CHECK (
   Caryophyllene >= 0 AND Caryophyllene <= 100 --Inclusive Percent
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Cohumulone_Range CHECK (
   Cohumulone >= 0 AND Cohumulone <= 100 --Inclusive Percent
)

ALTER TABLE Hop
ADD CONSTRAINT CK_Hop_Myrcene_Range CHECK (
   Myrcene >= 0 AND Myrcene <= 100 --Inclusive Percent
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_Yield_Range CHECK (
   Yield >= 0 AND Yield <= 100 --Inclusive Percent
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_Yield_Range CHECK (
   Yield >= 0 AND Yield <= 100 --Inclusive Percent
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_CoarseFineDiff_Range CHECK (
   CoarseFineDiff >= 0 AND CoarseFineDiff <= 100 --Inclusive Percent
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_CoarseFineDiff_Range CHECK (
   CoarseFineDiff >= 0 AND CoarseFineDiff <= 100 --Inclusive Percent
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_Moisture_Range CHECK (
   Moisture >= 0 AND Moisture <= 100 --Inclusive Percent
)
 
ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_Protein_Range CHECK (
   Protein >= 0 AND Protein <= 100 --Inclusive Percent
) 
      
ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_MaxInBatch_Range CHECK (
   MaxInBatch >= 0 AND MaxInBatch <= 100 --Inclusive Percent
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_DiastaticPower_Range CHECK (
   DiastaticPower >= 0 --Inclusive
)

ALTER TABLE Fermentable
ADD CONSTRAINT CK_Fermentable_IBUs_Range CHECK (
   IBUs >= 0 --Inclusive
)  
	
ALTER TABLE MashProfile
ADD CONSTRAINT CK_MashProfile_GrainTemp_Range CHECK (
   GrainTemp >= -50 AND GrainTemp <= 110 --Inclusive degrees Celsius.
)  	

ALTER TABLE MashProfile
ADD CONSTRAINT CK_MashProfile_TunTemp_Range CHECK (
   TunTemp >= -50 AND TunTemp <= 110 --Inclusive degrees Celsius.
)

ALTER TABLE MashProfile
ADD CONSTRAINT CK_MashProfile_SpargeTemp_Range CHECK (
   SpargeTemp >= -50 AND SpargeTemp <= 110 --Inclusive degrees Celsius.
)

ALTER TABLE MashProfile
ADD CONSTRAINT CK_MashProfile_PH_Range CHECK (
   PH >= -1 AND PH <= 11.5 --battery acid to ammonia 
)

ALTER TABLE MashProfile
ADD CONSTRAINT CK_MashProfile_TunWeight_Range CHECK (
   TunWeight >= 0 --Inclusive
) 

ALTER TABLE MashProfile
ADD CONSTRAINT CK_MashProfile_TunSpecificHeat_Range CHECK (
   TunSpecificHeat >= 0 --Inclusive
) 

ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_StepTemp_Range CHECK (
   SpargeTemp >= -50 AND SpargeTemp <= 110 --Inclusive degrees Celsius.
)

ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_StepTime_Range CHECK (
   StepTime >= 0 AND StepTime <= 1140 --Inclusive Time in Minutes.
)

ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_RampTime_Range CHECK (
   RampTime >= 0 AND RampTime <= 1140 --Inclusive Time in Minutes.
)

ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_InfuseTemp_Range CHECK (
   InfuseTemp >= -50 AND InfuseTemp <= 110 --Inclusive degrees Celsius.
)

ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_EndTemp_Range CHECK (
   EndTemp >= -50 AND EndTemp <= 110 --Inclusive degrees Celsius.
)

ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_SequenceNumber_Range CHECK (
   SequenceNumber >= 1 AND SequenceNumber <= 1000 --Inclusive Sequence
)
   
ALTER TABLE MashStep
ADD CONSTRAINT CK_MashStep_DecoctionAmount_Range CHECK (
   DecoctionAmount >= 0 AND DecoctionAmount <= 1000 --Inclusive Volume of mash to decoct
)

ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_Amount_Range CHECK (
   Amount >= 0 AND Amount <= 1000 --Inclusive Amount of yeast, measured in liters
)
     
ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_MinTemperature_Range CHECK (
   MinTemperature >= -50 AND MinTemperature <= 110 --Inclusive The minimum degrees Celsius
)	 
	 
ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_MaxTemperature_Range CHECK (
   MaxTemperature >= -50 AND MaxTemperature <= 110 --Inclusive The maximum degrees Celsius
)	 

ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_Attenuation_Range CHECK (
   Attenuation >= -50 AND Attenuation <= 150 --Inclusive Average attenuation for this yeast strain
)

ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_Attenuation_Range CHECK (
   Attenuation >= -50 AND Attenuation <= 150 --Inclusive Average attenuation for this yeast strain
)

ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_TimesCultured_Range CHECK (
   TimesCultured >= 0 AND TimesCultured <= 150 --Inclusive Number of times this yeast has been reused
)

ALTER TABLE Yeast
ADD CONSTRAINT CK_Yeast_MaxReuse_Range CHECK (
   MaxReuse >= 0 AND MaxReuse <= 150 --Inclusive Recommended of times this yeast
)
       
ALTER TABLE Recipe
ADD CONSTRAINT CK_Recipe_PrimayTemp_Range CHECK (
   PrimayTemp >= -50 AND PrimayTemp <= 110 --Inclusive The degrees Celsius
)

ALTER TABLE Recipe
ADD CONSTRAINT CK_Recipe_SecondaryTemp_Range CHECK (
   SecondaryTemp >= -50 AND SecondaryTemp <= 110 --Inclusive The degrees Celsius
)

ALTER TABLE Recipe
ADD CONSTRAINT CK_Recipe_TertiaryTemp_Range CHECK (
   TertiaryTemp >= -50 AND TertiaryTemp <= 110 --Inclusive The degrees Celsius
)

ALTER TABLE Recipe
ADD CONSTRAINT CK_Recipe_AgeTemp_Range CHECK (
   AgeTemp >= -50 AND AgeTemp <= 110 --Inclusive The degrees Celsius
)

ALTER TABLE Recipe
ADD CONSTRAINT CK_Recipe_CarbonationTemp_Range CHECK (
   CarbonationTemp >= -50 AND CarbonationTemp <= 110 --Inclusive The degrees Celsius
)	 
	 
ALTER TABLE RecipeFermentables 
ADD IsMashed BIT           DEFAULT ((0)) NOT NULL

ALTER TABLE RecipeFermentables 
ADD "AddAfterBoil" BIT           DEFAULT ((0)) NOT NULL

ALTER TABLE RecipeYeasts 
ADD AddToSecondary BIT           DEFAULT ((0)) NOT NULL

ALTER TABLE RecipeHops
ADD HopUses_Name NVARCHAR (75)  NULL

ALTER TABLE RecipeHops
ADD CONSTRAINT [FK_dbo.Hop_dbo.HopUse_HopUses_Name] FOREIGN KEY ([HopUses_Name]) REFERENCES [dbo].[HopUse] ([Name])