-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:8889
-- Généré le : jeu. 16 jan. 2025 à 09:07
-- Version du serveur : 5.7.39
-- Version de PHP : 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `bts_sio`
--

-- --------------------------------------------------------

--
-- Structure de la table `Allergies`
--

CREATE TABLE `Allergies` (
  `Id` int(11) NOT NULL,
  `Name` varchar(256) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `Allergies`
--

INSERT INTO `Allergies` (`Id`, `Name`) VALUES
(3, 'Lait'),
(4, 'Œufs'),
(5, 'Fruits de mer'),
(6, 'Acariens'),
(7, 'Poils d\'animaux'),
(8, 'Arachides'),
(9, 'Blé'),
(10, 'Soja'),
(11, 'Gluten'),
(12, 'Latex'),
(13, 'Piqûres d\'abeille'),
(14, 'Moisissure'),
(15, 'Pénicilline'),
(16, 'Parfum'),
(17, 'Nickel'),
(18, 'Venin d\'insectes'),
(19, 'Herbe'),
(20, 'Ambroisie'),
(21, 'Graines de sésame'),
(22, 'Poisson'),
(23, 'Fruits à coque'),
(24, 'Médicaments à base de sulfa'),
(25, 'Agrumes'),
(26, 'Chocolat'),
(27, 'Parfum'),
(28, 'Lumière du soleil'),
(29, 'Alcool'),
(30, 'Avocats');

-- --------------------------------------------------------

--
-- Structure de la table `AllergyMedicament`
--

CREATE TABLE `AllergyMedicament` (
  `AllergiesId` int(11) NOT NULL,
  `MedicamentsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `AllergyMedicament`
--

INSERT INTO `AllergyMedicament` (`AllergiesId`, `MedicamentsId`) VALUES
(8, 1),
(11, 1);

-- --------------------------------------------------------

--
-- Structure de la table `AspNetRoleClaims`
--

CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `AspNetRoles`
--

CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `AspNetUserClaims`
--

CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `AspNetUserLogins`
--

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `AspNetUserRoles`
--

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `AspNetUsers`
--

CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) NOT NULL,
  `FirstName` longtext NOT NULL,
  `LastName` longtext NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `AspNetUsers`
--

INSERT INTO `AspNetUsers` (`Id`, `FirstName`, `LastName`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('2c7b1bee-8664-46db-8c57-573c117eabd9', 'Abdurahmen', 'GHARSALLI', 'abdurahmen', 'ABDURAHMEN', 'abdurahmengharsalli@gmail.com', 'ABDURAHMENGHARSALLI@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEP2CgqJfqDO/c8mMVG01zq/L2TmwvxHQ2wOxfspYlSTiBUl4yxJIouYkOwgdlTyLTQ==', 'HGLTJWRQIYAKJL7VDGTS5QXNTMPEODQL', '3204ee4e-749c-4ade-9e70-6c936a539315', NULL, 0, 0, NULL, 1, 0),
('6fcaf5cd-c8ff-40f1-84ed-3343610855f2', 'Safwane', 'REMADI', 'safwane', 'safwane', 'saf.alg699@gmail.com', 'saf.alg699@gmail.com', 0, 'AQAAAAIAAYagAAAAELP9GsDPCk9PiukUgFBWEt0joAb8paxNNNxZrxjJvkSKSRMu5z/5FOSiPTV6ZRwhGA==', 'OHETRHZKEG6UXLGFAAAZST4EJZ5G257N', '5b167865-bacb-44e8-a37f-26abdd6cac98', NULL, 0, 0, NULL, 1, 0),
('fa86ecae-b6b0-4fe9-9e2c-9edd932928fa', 'Nicolas', 'PONS', 'nicolas', 'NICOLAS', 'nicolaspons@gmail.com', 'NICOLASPONS@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAENcjLZV3TP6MrTC1iDpDJ8qk1Nn2CWJYQSBd7QFbhf9D1gfRClDtS9WeBckfTgTiig==', 'G5SC6AKP54SZA643T2JNO5PSERMS53OJ', 'e3d9a974-79cf-4d1a-9743-63fd628bcf7c', NULL, 0, 0, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `AspNetUserTokens`
--

CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `MedicalHistories`
--

CREATE TABLE `MedicalHistories` (
  `Id` int(11) NOT NULL,
  `Name` varchar(256) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `MedicalHistories`
--

INSERT INTO `MedicalHistories` (`Id`, `Name`) VALUES
(2, 'Diabète de type 1'),
(3, 'Diabète de type 2'),
(4, 'Asthme'),
(5, 'Cancer du poumon'),
(6, 'Insuffisance rénale chronique'),
(7, 'Infarctus du myocarde'),
(8, 'Accident vasculaire cérébral'),
(9, 'Cholestérol élevé'),
(10, 'Maladie de Parkinson'),
(11, 'Maladie d\'Alzheimer'),
(12, 'Sclérose en plaques'),
(13, 'Hépatite B'),
(14, 'Hépatite C'),
(15, 'Tuberculose'),
(16, 'Anémie falciforme'),
(17, 'Arthrose'),
(18, 'Polyarthrite rhumatoïde'),
(19, 'Insuffisance cardiaque'),
(20, 'Obésité'),
(21, 'Migraine chronique'),
(22, 'Apnée du sommeil'),
(23, 'Dépression'),
(24, 'Trouble bipolaire'),
(25, 'Schizophrénie'),
(26, 'Trouble d\'anxiété généralisée'),
(27, 'Ostéoporose'),
(28, 'Maladie de Crohn'),
(29, 'Colite ulcéreuse'),
(30, 'Maladie coronarienne');

-- --------------------------------------------------------

--
-- Structure de la table `MedicalHistoryMedicament`
--

CREATE TABLE `MedicalHistoryMedicament` (
  `MedicalHistoriesId` int(11) NOT NULL,
  `MedicamentsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `MedicalHistoryMedicament`
--

INSERT INTO `MedicalHistoryMedicament` (`MedicalHistoriesId`, `MedicamentsId`) VALUES
(4, 1),
(12, 1);

-- --------------------------------------------------------

--
-- Structure de la table `MedicalHistoryPatient`
--

CREATE TABLE `MedicalHistoryPatient` (
  `MedicalHistoriesId` int(11) NOT NULL,
  `PatientsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `MedicalHistoryPatient`
--

INSERT INTO `MedicalHistoryPatient` (`MedicalHistoriesId`, `PatientsId`) VALUES
(4, 1),
(7, 1),
(8, 1),
(12, 1),
(3, 3),
(5, 3),
(6, 3),
(7, 3),
(8, 3),
(9, 3),
(10, 3),
(12, 3);

-- --------------------------------------------------------

--
-- Structure de la table `MedicamentPrescription`
--

CREATE TABLE `MedicamentPrescription` (
  `MedicamentsId` int(11) NOT NULL,
  `PrescriptionsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `MedicamentPrescription`
--

INSERT INTO `MedicamentPrescription` (`MedicamentsId`, `PrescriptionsId`) VALUES
(5, 20),
(7, 20),
(9, 20),
(10, 20),
(13, 20),
(5, 21),
(8, 21),
(10, 21),
(13, 21),
(15, 21),
(17, 21),
(7, 22),
(8, 22),
(6, 23),
(8, 23),
(11, 23),
(12, 23),
(14, 23),
(5, 24),
(7, 24),
(11, 24),
(13, 24),
(18, 24);

-- --------------------------------------------------------

--
-- Structure de la table `Medicaments`
--

CREATE TABLE `Medicaments` (
  `Id` int(11) NOT NULL,
  `Name` varchar(256) NOT NULL,
  `Quantity` varchar(1024) NOT NULL,
  `Ingredients` varchar(1024) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `Medicaments`
--

INSERT INTO `Medicaments` (`Id`, `Name`, `Quantity`, `Ingredients`) VALUES
(1, 'Paracetamol', '500mg', 'Paracetamol'),
(4, 'Amoxicillin', '250mg', 'Amoxicilline'),
(5, 'Ciprofloxacin', '500mg', 'Ciprofloxacine'),
(6, 'Metformin', '850mg', 'Metformin'),
(7, 'Omeprazole', '20mg', 'Omeprazole'),
(8, 'Lisinopril', '10mg', 'Lisinopril'),
(9, 'Simvastatin', '40mg', 'Simvastatin'),
(10, 'Levothyroxine', '100mcg', 'Levothyroxine'),
(11, 'Citalopram', '20mg', 'Citalopram'),
(12, 'Furosemide', '40mg', 'Furosemide'),
(13, 'Albuterol', '90mcg', 'Albuterol'),
(14, 'Amlodipine', '5mg', 'Amlodipine'),
(15, 'Clopidogrel', '75mg', 'Clopidogrel'),
(16, 'Metoprolol', '50mg', 'Metoprolol'),
(17, 'Losartan', '50mg', 'Losartan'),
(18, 'Atorvastatin', '20mg', 'Atorvastatin'),
(19, 'Hydrochlorothiazide', '25mg', 'Hydrochlorothiazide'),
(20, 'Gabapentin', '300mg', 'Gabapentin');

-- --------------------------------------------------------

--
-- Structure de la table `PatientAllergy`
--

CREATE TABLE `PatientAllergy` (
  `AllergiesId` int(11) NOT NULL,
  `PatientsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `PatientAllergy`
--

INSERT INTO `PatientAllergy` (`AllergiesId`, `PatientsId`) VALUES
(3, 1),
(5, 1),
(6, 1),
(9, 1),
(3, 3),
(6, 3),
(7, 3),
(8, 3),
(10, 3),
(12, 3),
(16, 3),
(19, 3);

-- --------------------------------------------------------

--
-- Structure de la table `Patients`
--

CREATE TABLE `Patients` (
  `Id` int(11) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Age` int(11) NOT NULL,
  `Gender` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `Weight` int(11) NOT NULL,
  `SecurityCardNumber` int(11) NOT NULL,
  `DoctorId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `Patients`
--

INSERT INTO `Patients` (`Id`, `FirstName`, `LastName`, `Age`, `Gender`, `Height`, `Weight`, `SecurityCardNumber`, `DoctorId`) VALUES
(1, 'Nicolas', 'PONS', 32, 0, 175, 70, 9213451, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2'),
(2, 'Clement', 'QUIQUANDON', 25, 0, 170, 60, 234567, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2'),
(3, 'Abdurahmen', 'GHARSALLI', 19, 0, 180, 90, 3456789, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2');

-- --------------------------------------------------------

--
-- Structure de la table `Prescriptions`
--

CREATE TABLE `Prescriptions` (
  `Id` int(11) NOT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `Dosage` longtext,
  `AdditionalInformation` longtext,
  `PatientId` int(11) NOT NULL,
  `DoctorId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `Prescriptions`
--

INSERT INTO `Prescriptions` (`Id`, `StartDate`, `EndDate`, `Dosage`, `AdditionalInformation`, `PatientId`, `DoctorId`) VALUES
(20, '2024-11-22', '2024-12-22', '2 fois par jour', NULL, 2, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2'),
(21, '2024-11-22', '2024-12-22', 'Matin midi soir', NULL, 1, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2'),
(22, '2024-11-22', '2024-12-22', 'une fois par jour', NULL, 3, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2'),
(23, '2024-11-22', '2024-12-22', 'Matin midi soir', NULL, 1, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2'),
(24, '2024-11-22', '2024-12-22', '2 fois par jour', 'Dois faire une séance de sport chaque jour', 1, '6fcaf5cd-c8ff-40f1-84ed-3343610855f2');

-- --------------------------------------------------------

--
-- Structure de la table `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20241112135142_InitialCreate', '8.0.1');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `Allergies`
--
ALTER TABLE `Allergies`
  ADD PRIMARY KEY (`Id`);

--
-- Index pour la table `AllergyMedicament`
--
ALTER TABLE `AllergyMedicament`
  ADD PRIMARY KEY (`AllergiesId`,`MedicamentsId`),
  ADD KEY `IX_AllergyMedicament_MedicamentsId` (`MedicamentsId`);

--
-- Index pour la table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Index pour la table `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Index pour la table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Index pour la table `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Index pour la table `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Index pour la table `AspNetUsers`
--
ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Index pour la table `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Index pour la table `MedicalHistories`
--
ALTER TABLE `MedicalHistories`
  ADD PRIMARY KEY (`Id`);

--
-- Index pour la table `MedicalHistoryMedicament`
--
ALTER TABLE `MedicalHistoryMedicament`
  ADD PRIMARY KEY (`MedicalHistoriesId`,`MedicamentsId`),
  ADD KEY `IX_MedicalHistoryMedicament_MedicamentsId` (`MedicamentsId`);

--
-- Index pour la table `MedicalHistoryPatient`
--
ALTER TABLE `MedicalHistoryPatient`
  ADD PRIMARY KEY (`MedicalHistoriesId`,`PatientsId`),
  ADD KEY `IX_MedicalHistoryPatient_PatientsId` (`PatientsId`);

--
-- Index pour la table `MedicamentPrescription`
--
ALTER TABLE `MedicamentPrescription`
  ADD PRIMARY KEY (`MedicamentsId`,`PrescriptionsId`),
  ADD KEY `IX_MedicamentPrescription_PrescriptionsId` (`PrescriptionsId`);

--
-- Index pour la table `Medicaments`
--
ALTER TABLE `Medicaments`
  ADD PRIMARY KEY (`Id`);

--
-- Index pour la table `PatientAllergy`
--
ALTER TABLE `PatientAllergy`
  ADD PRIMARY KEY (`AllergiesId`,`PatientsId`),
  ADD KEY `IX_PatientAllergy_PatientsId` (`PatientsId`);

--
-- Index pour la table `Patients`
--
ALTER TABLE `Patients`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Patients_DoctorId` (`DoctorId`);

--
-- Index pour la table `Prescriptions`
--
ALTER TABLE `Prescriptions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Prescriptions_DoctorId` (`DoctorId`),
  ADD KEY `IX_Prescriptions_PatientId` (`PatientId`);

--
-- Index pour la table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `Allergies`
--
ALTER TABLE `Allergies`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pour la table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `MedicalHistories`
--
ALTER TABLE `MedicalHistories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pour la table `Medicaments`
--
ALTER TABLE `Medicaments`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT pour la table `Patients`
--
ALTER TABLE `Patients`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `Prescriptions`
--
ALTER TABLE `Prescriptions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `AllergyMedicament`
--
ALTER TABLE `AllergyMedicament`
  ADD CONSTRAINT `FK_AllergyMedicament_Allergies_AllergiesId` FOREIGN KEY (`AllergiesId`) REFERENCES `Allergies` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AllergyMedicament_Medicaments_MedicamentsId` FOREIGN KEY (`MedicamentsId`) REFERENCES `Medicaments` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `MedicalHistoryMedicament`
--
ALTER TABLE `MedicalHistoryMedicament`
  ADD CONSTRAINT `FK_MedicalHistoryMedicament_MedicalHistories_MedicalHistoriesId` FOREIGN KEY (`MedicalHistoriesId`) REFERENCES `MedicalHistories` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_MedicalHistoryMedicament_Medicaments_MedicamentsId` FOREIGN KEY (`MedicamentsId`) REFERENCES `Medicaments` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `MedicalHistoryPatient`
--
ALTER TABLE `MedicalHistoryPatient`
  ADD CONSTRAINT `FK_MedicalHistoryPatient_MedicalHistories_MedicalHistoriesId` FOREIGN KEY (`MedicalHistoriesId`) REFERENCES `MedicalHistories` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_MedicalHistoryPatient_Patients_PatientsId` FOREIGN KEY (`PatientsId`) REFERENCES `Patients` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `MedicamentPrescription`
--
ALTER TABLE `MedicamentPrescription`
  ADD CONSTRAINT `FK_MedicamentPrescription_Medicaments_MedicamentsId` FOREIGN KEY (`MedicamentsId`) REFERENCES `Medicaments` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_MedicamentPrescription_Prescriptions_PrescriptionsId` FOREIGN KEY (`PrescriptionsId`) REFERENCES `Prescriptions` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `PatientAllergy`
--
ALTER TABLE `PatientAllergy`
  ADD CONSTRAINT `FK_PatientAllergy_Allergies_AllergiesId` FOREIGN KEY (`AllergiesId`) REFERENCES `Allergies` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_PatientAllergy_Patients_PatientsId` FOREIGN KEY (`PatientsId`) REFERENCES `Patients` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `Patients`
--
ALTER TABLE `Patients`
  ADD CONSTRAINT `FK_Patients_AspNetUsers_DoctorId` FOREIGN KEY (`DoctorId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `Prescriptions`
--
ALTER TABLE `Prescriptions`
  ADD CONSTRAINT `FK_Prescriptions_AspNetUsers_DoctorId` FOREIGN KEY (`DoctorId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Prescriptions_Patients_PatientId` FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
