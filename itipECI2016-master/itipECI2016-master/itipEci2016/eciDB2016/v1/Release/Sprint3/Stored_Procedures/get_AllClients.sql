﻿/***********************************************************************************************************
Description: Stored Procedure to pull information from Clients table
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	6.21.16
Change History:
	06-30-2016 -- jmg -- Generated altID.
	07-05-2016 -- removed generated altID. Updated stored proc to new column names for compliance.
************************************************************************************************************/
ALTER PROCEDURE [dbo].[get_AllClients]

AS
	BEGIN
		BEGIN TRY

			SELECT clnt.*,
				rce.race,
				eth.ethnicity,
				sts.clientStatus,
				dx.diagnosisCode,
				dx.diagnosis,
				plang.primaryLanguage,
				sclinf.isd,
				insauth.insuranceAuthorizationType,
				insauth.authorized_From,
				insauth.authorized_To,
				comprf.communicationPreferences

			FROM Clients clnt
			LEFT JOIN Race rce
				ON clnt.raceID = rce.raceID
			LEFT JOIN Ethnicity eth
				ON clnt.ethnicityID = eth.ethnicityID
			LEFT JOIN ClientStatus sts
				ON clnt.clientStatusID = sts.clientStatusID
			LEFT JOIN Diagnosis dx
				ON clnt.diagnosisID = dx.diagnosisID
			LEFT JOIN PrimaryLanguage plang
				ON clnt.primaryLanguageID = plang.primaryLanguageID
			LEFT JOIN SchoolInformation sclinf
				ON clnt.schoolInfoID = sclinf.schoolInfoID
			LEFT JOIN InsuranceAuthorization insauth
				ON clnt.insuranceAuthID = insauth.insuranceAuthID
			LEFT JOIN CommunicationPreferences comprf
				ON clnt.communicationPreferencesID = comprf.communicationPreferencesID

		END TRY
		BEGIN CATCH

			DECLARE @timeStamp DATETIME,
				@errorMessage VARCHAR(255),
				@errorProcedure VARCHAR(100)	

			SELECT @timeStamp = GETDATE(),
					@errorMessage = ERROR_MESSAGE(),
					@errorProcedure = ERROR_PROCEDURE()
			
			EXECUTE dbo.log_ErrorTimeStamp @timeStamp, @errorMessage, @errorProcedure

		END CATCH
	END