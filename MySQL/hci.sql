-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Korisnik`
-- -----------------------------------------------------

CREATE TABLE IF NOT EXISTS `mydb`.`Korisnik` (
  `idKorisnik` INT  NOT NULL,
  `Ime` VARCHAR(45) NOT NULL,
  `Prezime` VARCHAR(45) NULL,
  `Email` VARCHAR(45) NULL,
  `Username` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(255) NOT NULL,
   `Tema` VARCHAR(255) NOT NULL default  "Blue",
  `Tip_korisnika` ENUM('student', 'profesor', 'administrator') NULL,
  PRIMARY KEY (`idKorisnik`),
  UNIQUE INDEX `Username_UNIQUE` (`Username` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Predmet`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Predmet` (
  `idPredmeta` INT NOT NULL ,
  `Naziv` VARCHAR(45) NOT NULL,
  `Opis` VARCHAR(600) NULL,
  `ECTS` INT NULL,
  UNIQUE INDEX `idPredmet_UNIQUE` (`idPredmeta` ASC) VISIBLE,
  PRIMARY KEY (`idPredmeta`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Student`

-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Student` (
  `BrojIndeksa` VARCHAR(45) NOT NULL,
  `GodinaStudija` INT NOT NULL,
  `Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`Korisnik_idKorisnik`),
  UNIQUE INDEX `brojIndeksa_UNIQUE` (`BrojIndeksa` ASC) VISIBLE,
  INDEX `fk_Student_Korisnik1_idx` (`Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_Student_Korisnik1`
    FOREIGN KEY (`Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Korisnik` (`idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Profesor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Profesor` (
  `Zvanje` VARCHAR(45) NULL,
  `Korisnik_idKorisnik` INT NOT NULL,
  INDEX `fk_Profesor_Korisnik1_idx` (`Korisnik_idKorisnik` ASC) VISIBLE,
  PRIMARY KEY (`Korisnik_idKorisnik`),
  CONSTRAINT `fk_Profesor_Korisnik1`
    FOREIGN KEY (`Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Korisnik` (`idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Predmet_Student`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Predmet_Student` (
  `Predmet_idPredmeta` INT NOT NULL,
  `DatumUpisa` DATETIME NOT NULL,
  `Student_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`Predmet_idPredmeta`, `Student_Korisnik_idKorisnik`),
  INDEX `fk_Predmet_has_Student_Predmet_idx` (`Predmet_idPredmeta` ASC) VISIBLE,
  INDEX `fk_Predmet_Student_Student1_idx` (`Student_Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_Predmet_has_Student_Predmet`
    FOREIGN KEY (`Predmet_idPredmeta`)
    REFERENCES `mydb`.`Predmet` (`idPredmeta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Predmet_Student_Student1`
    FOREIGN KEY (`Student_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Student` (`Korisnik_idKorisnik`)
   ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`DomaciZadatak`
-- -----------------------------------------------------

CREATE TABLE IF NOT EXISTS `mydb`.`DomaciZadatak` (
  `idDomaciZadatak` VARCHAR(45) NOT NULL ,
  `Naziv` VARCHAR(45) NOT NULL,
  `Opis` MEDIUMTEXT NULL,
  `Rok` DATETIME NOT NULL,
  `Predmet_idPredmeta` INT NOT NULL,
  `Profesor_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`idDomaciZadatak`),
  INDEX `fk_DomaciZadatak_Predmet1_idx` (`Predmet_idPredmeta` ASC) VISIBLE,
  INDEX `fk_DomaciZadatak_Profesor1_idx` (`Profesor_Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_DomaciZadatak_Predmet1`
    FOREIGN KEY (`Predmet_idPredmeta`)
    REFERENCES `mydb`.`Predmet` (`idPredmeta`)
    ON DELETE  NO ACTION
    ON UPDATE  NO ACTION,
  CONSTRAINT `fk_DomaciZadatak_Profesor1`
    FOREIGN KEY (`Profesor_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Profesor` (`Korisnik_idKorisnik`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Ispit`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Ispit` (
  `DatumIspita` DATETIME NOT NULL,
  `Predmet_idPredmeta` INT NOT NULL,
  PRIMARY KEY (`Predmet_idPredmeta`, `DatumIspita`),
  INDEX `fk_Ispit_Predmet1_idx` (`Predmet_idPredmeta` ASC) VISIBLE,
  CONSTRAINT `fk_Ispit_Predmet1`
    FOREIGN KEY (`Predmet_idPredmeta`)
    REFERENCES `mydb`.`Predmet` (`idPredmeta`)
     ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Ocjena`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Ocjena` (
  `idOcjena` INT UNSIGNED NOT NULL,
  `Bodovi` VARCHAR(45) NULL,
  `Ocjena` DECIMAL NULL,
  `Datum` DATETIME NULL,
  `Predmet_idPredmeta` INT NOT NULL,
  `Profesor_Korisnik_idKorisnik` INT NOT NULL,
  `Student_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`idOcjena`),
  INDEX `fk_Ocjena_Predmet1_idx` (`Predmet_idPredmeta` ASC) VISIBLE,
  INDEX `fk_Ocjena_Profesor1_idx` (`Profesor_Korisnik_idKorisnik` ASC) VISIBLE,
  INDEX `fk_Ocjena_Student1_idx` (`Student_Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_Ocjena_Predmet1`
    FOREIGN KEY (`Predmet_idPredmeta`)
    REFERENCES `mydb`.`Predmet` (`idPredmeta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Ocjena_Profesor1`
    FOREIGN KEY (`Profesor_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Profesor` (`Korisnik_idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Ocjena_Student1`
    FOREIGN KEY (`Student_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Student` (`Korisnik_idKorisnik`)
     ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`DomaciZadatak_Student`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`DomaciZadatak_Student` (
  `Bodovi` DECIMAL NULL,
  `DomaciZadatak_IdDomaciZadatak` varchar(45) NOT NULL,
  `Student_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`DomaciZadatak_IdDomaciZadatak`, `Student_Korisnik_idKorisnik`),
  INDEX `fk_DomaciZadatak_Student_Student1_idx` (`Student_Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_DomaciZadatak_has_Student_DomaciZadatak1`
    FOREIGN KEY (`DomaciZadatak_IdDomaciZadatak`)
    REFERENCES `mydb`.`DomaciZadatak` (`idDomaciZadatak`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_DomaciZadatak_Student_Student1`
    FOREIGN KEY (`Student_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Student` (`Korisnik_idKorisnik`)
     ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Prisustvo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Prisustvo` (
  `Datum` DATETIME NOT NULL,
  `Status` ENUM('prisutan', 'odustan') NULL,
  `Predmet_idPredmeta` INT NOT NULL,
  `Student_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`Datum`, `Predmet_idPredmeta`, `Student_Korisnik_idKorisnik`),
  INDEX `fk_Prisustvo_Predmet1_idx` (`Predmet_idPredmeta` ASC) VISIBLE,
  INDEX `fk_Prisustvo_Student1_idx` (`Student_Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_Prisustvo_Predmet1`
    FOREIGN KEY (`Predmet_idPredmeta`)
    REFERENCES `mydb`.`Predmet` (`idPredmeta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Prisustvo_Student1`
    FOREIGN KEY (`Student_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Student` (`Korisnik_idKorisnik`)
     ON DELETE CASCADE
    ON UPDATE CASCADE
)
ENGINE = InnoDB;



-- -----------------------------------------------------
-- Table `mydb`.`Ispit_Student`
-- -----------------------------------------------------

CREATE TABLE IF NOT EXISTS mydb.Ispit_Student (
  Bodovi DECIMAL NULL,
  Ocjena INT,
  Ispit_DatumIspita DATETIME NOT NULL,
  Student_Korisnik_idKorisnik INT NOT NULL,
  Predmet_idPredmeta INT NOT NULL,
  PRIMARY KEY (Ispit_DatumIspita, Student_Korisnik_idKorisnik, Predmet_idPredmeta),
  INDEX fk_Ispit_Student_Student1_idx (Student_Korisnik_idKorisnik ASC) VISIBLE,
  INDEX fk_Ispit_Student_Predmet1_idx (Predmet_idPredmeta ASC) VISIBLE,
  CONSTRAINT fk_Ispit_Student_Student1
    FOREIGN KEY (Student_Korisnik_idKorisnik)
    REFERENCES mydb.Student (Korisnik_idKorisnik)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Ispit_Student_Predmet1
    FOREIGN KEY (Predmet_idPredmeta)
    REFERENCES mydb.Predmet (idPredmeta)
     ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Predmet_has_Profesor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Predmet_Profesor` (
  `Predmet_idPredmeta` INT NOT NULL,
  `Profesor_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`Predmet_idPredmeta`, `Profesor_Korisnik_idKorisnik`),
  INDEX `fk_Predmet_has_Profesor_Profesor1_idx` (`Profesor_Korisnik_idKorisnik` ASC) VISIBLE,
  INDEX `fk_Predmet_has_Profesor_Predmet1_idx` (`Predmet_idPredmeta` ASC) VISIBLE,
  CONSTRAINT `fk_Predmet_has_Profesor_Predmet1`
    FOREIGN KEY (`Predmet_idPredmeta`)
    REFERENCES `mydb`.`Predmet` (`idPredmeta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Predmet_has_Profesor_Profesor1`
    FOREIGN KEY (`Profesor_Korisnik_idKorisnik`)
    REFERENCES `mydb`.`Profesor` (`Korisnik_idKorisnik`)
      ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


INSERT INTO `mydb`.`Korisnik` 
(`idKorisnik`, `Ime`, `Prezime`, `Email`, `Username`, `Password`, `Tip_korisnika`) 
VALUES
(0, 'Admin', 'Admin', 'admin@email.com', 'admin', '$2a$12$mBPdBJSUPIcpKGuG8kAf6O/giymXPO5.GAknof5UQ2NGYxT.8h7dy', 'administrator'),
(1, 'Jovan', 'Petrović', 'jovan.petrovic@email.com', 'jovan123', '$2a$12$VwH73VJmK4ectOqemggDR.mDoPXLU8v28ZeR9gIOIYt0x9CMxsfg6', 'student'),
(2, 'Marija', 'Ivić', 'marija.ivic@email.com', 'marija456', '$2a$12$K3wBNwFdANRaKLl9FUIBHeY47jNhI3Qz8LURVvq5GcnBRVP5Sdlvq', 'profesor'),
(3, 'Aleksandar', 'Marković', 'aleksandar.markovic@email.com', 'alex789', '$2a$12$Cbka1wlZTw6eSUvDnb3.9eCBrJPbMSjdgI9stOd2sMk8E/FiSak0u', 'student'),
(4, 'Nikola', 'Jovanović', 'nikola.jovanovic@email.com', 'nikola001', '$2b$12$8eVj6QuRym4iKSwtJEXh0e69I4yS7VrPy.HBEMNhab1eWBM9MjMbS', 'student'),
(5, 'Ana', 'Kovačević', 'ana.kovacevic@email.com', 'ana.k', '$2b$12$pO0LN9BmWVYuGAcwAWda0Oq85oJojt5JhgzK1kszakwVdNXHdGGCi', 'student'),
(6, 'Petar', 'Milić', 'petar.milic@email.com', 'petarM', '$2b$12$9EhO5uT/Yaqi0WCkPDlK5utnFnLh/iaBxyDHqrHknqePfCCxvgik6', 'profesor'),
(7, 'Ivana', 'Radić', 'ivana.radic@email.com', 'ivanaR', '$2b$12$rsDNXE7Wairzpg9MfsNvVevtq1bTPE0B.ps2Z/QCl57HpOo5u5qGG', 'student'),
(8, 'Miloš', 'Tomić', 'milos.tomic@email.com', 'milosT', '$2b$12$kooS9g9vm4i3mCPHmr21O.yWsb1S8qPXjUqcVFyG8/n1YgdW5PEh2', 'profesor'),
(9, 'Jelena', 'Stanković', 'jelena.stankovic@email.com', 'jelenaS', '$2b$12$fJmeLk/r5RmS0srSj9V6/eydF3B//fME4xamRV1ocQ1jAj4kTN4FS', 'student'),
(10, 'Stefan', 'Nikolić', 'stefan.nikolic@email.com', 'stefanN', '$2b$12$n6OYJV6TkomCfOn2oZCVvunwWg7yEAJNdyf5vsswiR1LZihv8YZm.', 'student'),
(11, 'Tamara', 'Lazić', 'tamara.lazic@email.com', 'tamaraL', '$2b$12$JKbcRU/Fjn7BRWrA2x/7i.HytkuLLyXQBETmvXa3KRjcleFqfdrLW', 'student'),
(12, 'Marko', 'Vuković', 'marko.vukovic@email.com', 'markoV', '$2b$12$HMEz4O50NN6i7HBEO3K.0OQ0ebgNVMbexpZw0REgK4z78foDD81ZG', 'profesor'),
(13, 'Sara', 'Mitrović', 'sara.mitrovic@email.com', 'saraM', '$2b$12$tFrnM0C.gqcenZjAnWa/dOxIkmJ92p40yNwbnWFlIyzI.pAys8kaW', 'student'),
(14, 'Luka', 'Janković', 'luka.jankovic@email.com', 'lukaJ', '$2b$12$7RJZUVqvJO3nP.tLoOd/QOXfVKXlojoEhJF.fk9vpU355q/nTATua', 'student'),
(15, 'Milica', 'Obradović', 'milica.obradovic@email.com', 'milicaO', '$2b$12$Afov4c7iNLgIyJY2HD0Bb.wQxm6lCQJKZO5paJTQ.6H5zMBBT//A.', 'student'),
(16, 'Dragan', 'Pavlović', 'dragan.pavlovic@email.com', 'draganP', '$2b$12$jUTe.3lzCWfpuO5V9DNnq.JathDT1EzXl55I.EaN97QkUjuJPn2s.', 'profesor'),
(17, 'Teodora', 'Nikšić', 'teodora.niksic@email.com', 'teodoraN', '$2b$12$82oCH1yaKNilMWOpdQb6GupvHzcWm.e1t3Ef6/h3F2zyJ9xSjAOUC', 'student'),
(18, 'Nemanja', 'Ristić', 'nemanja.ristic@email.com', 'nemanjaR', '$2b$12$w5oenhBlO8J7y5VfUDiGdeFKgtHKFlYUNnx4FUodh14lRLWIoCIxm', 'profesor'),
(19, 'Kristina', 'Savić', 'kristina.savic@email.com', 'kristinaS', '$2b$12$34b/xNq5TSdcAnP/R0sLRu.i7b5znRnZBiLUiPod4REtji01Eluwe', 'student'),
(20, 'Filip', 'Bogdanović', 'filip.bogdanovic@email.com', 'filipB', '$2b$12$.Yf/XsAFy5uUMwie5hqmaeV3jYcSbu5GGD0Vqfhzi/pLbamqtlmwW', 'student'),
(21, 'Nenad', 'Rakić', 'nenad.rakic@email.com', 'nenad215', '$2b$12$f7Er9evbAro.h2vYZj2Tle9CMHgTzCknOef7W48YJ3ywbzBYKJcnu', 'student'),
(22, 'Iva', 'Đukić', 'iva.djukić@email.com', 'iva227', '$2b$12$LGDydvwTRDnBgXO1nifKWePhdHnYZhNdZJ99KQzF2tsnv64lAzacG', 'profesor'),
(23, 'Bojan', 'Blagojević', 'bojan.blagojevic@email.com', 'bojan237', '$2b$12$6Sn/HEuvCqkhEOiOVLzm5eLqOSoUCzAlRwA2E.CPT7pgAkPa63I7i', 'student'),
(24, 'Katarina', 'Zorić', 'katarina.zoric@email.com', 'katarina243', '$2b$12$eqhEVfYoMKwe9dn4BJsiUuNdCRGHoSml9qQ0VY9MBRG0sVZlC4M8q', 'profesor'),
(25, 'Vladimir', 'Jelić', 'vladimir.jelic@email.com', 'vladimir259', '$2b$12$GRbi/1q/q7SThw7pYRp4vuDmt9FtK6YJPXBw0L5Ff8/Fk0sLYm2cu', 'profesor'),
(26, 'Lana', 'Marinković', 'lana.marinkovic@email.com', 'lana264', '$2b$12$dZ2wA/2o4yIl0/EDJAG1ZujyGq0CLjICcK0RRTr0K6Q0gZIjK9bhq', 'student'),
(27, 'Ognjen', 'Vučić', 'ognjen.vucic@email.com', 'ognjen279', '$2b$12$OXgmYkUbdIVZgJYhXnWxOeMoEolOHpQydnB4KEU0pMvIE4I9iNRtS', 'profesor'),
(28, 'Maja', 'Tadić', 'maja.tadic@email.com', 'maja286', '$2b$12$YjPDeZDr1B4XJdx0xy1k8.4R/vNB1bz5huEqVBl9vTD9TIJAB9Qo2', 'student'),
(29, 'Milan', 'Obućina', 'milan.obucina@email.com', 'milan293', '$2b$12$A1lsgfh3ZoZWc3QktpjK8eRoVOAmzz6ShT9tw7aDLBfZnwv4X0lZC', 'profesor'),
(30, 'Anđela', 'Grbić', 'andjela.grbic@email.com', 'andjela300', '$2b$12$BFG8My0gLMk9KM34pvUM7u6gB7K25Q4LUPux7zDA5A1g1rLMOU1QC', 'student');



INSERT INTO `mydb`.`Predmet` (`idPredmeta`, `Naziv`, `Opis`, `ECTS`) VALUES
(2257, 'Programiranje u Javi', 'Osnove programiranja u Javi', 6),
(2222, 'Osnove baze podataka', 'Teorija i praksa baza podataka', 5),
(3412, 'Matematika 1', 'Osnovni matematički pojmovi i operacije', 7),
(1205, 'Algoritmi i strukture podataka', 'Analiza i implementacija algoritama i struktura podataka', 6),
(1309, 'Operativni sistemi', 'Principi rada operativnih sistema i upravljanje resursima', 5),
(2103, 'Računarska mreža', 'Osnove mrežnih protokola i arhitektura', 6),
(4110, 'Mašinsko učenje', 'Metode i algoritmi mašinskog učenja', 6),
(3125, 'Razvoj web aplikacija', 'Frontend i backend razvoj modernih web aplikacija', 5),
(5107, 'Softverski inženjering', 'Metodologije i procesi razvoja softvera', 6),
(3413, 'Matematika 2', 'Matematička analiza', 6),
(5108, 'Matematika 4', 'Linearna algebra', 6),
(3411, 'Diskretna matematika', 'Diskretna matematika', 7),
(2233, 'Industrijsko preduzetništvo', 'Poslovna logika', 6),
(5100, 'Informacioni sistemi', 'Metodologije i procesi razvoja informacionih sistema', 5),
(2223, 'Osnovi elektrotehnike 1', 'Elektrostatika', 6),
(2224, 'Osnovi elektrotehnike 2', 'Osnove električnih kola i uvod u magnetiku', 6),
(2225, 'Osnovi magnetizma', 'Osnove magnetike', 6),
(6088, 'Sigurnost informacionih sistema', 'Principi zaštite podataka i sigurnosti sistema', 5),
(6201, 'Veštačka inteligencija', 'Uvod u koncepte i algoritme veštačke inteligencije', 6),
(6202, 'Mobilne aplikacije', 'Razvoj aplikacija za Android i iOS platforme', 5),
(6203, 'Računarska grafika', 'Osnovni principi prikaza slike i 3D modelovanja', 6),
(6204, 'Napredne baze podataka', 'Optimizacija, distribuirane baze i NoSQL', 5),
(6205, 'Teorija računanja', 'Formalni jezici, automati i kompleksnost', 6);


INSERT INTO `mydb`.`Student` (`BrojIndeksa`, `GodinaStudija`, `Korisnik_idKorisnik`) VALUES
('S12345', 2, 1),
('S23456', 1, 3),
('S34567', 3, 4),
('S45678', 2, 5),
('S51789', 4, 7),
('S56789', 4, 9),
('S56189', 4, 10),
('S57189', 4, 11),
('S58789', 4, 13),
('S59789', 4, 14),
('S66789', 4, 17),
('S86789', 4, 19),
('S76789', 4, 20),
('S86719', 4, 21),
('S96789', 4, 23),
('S16789', 4, 25),
('S26789', 4, 26),
('S36789', 4, 28),
('S17890', 1, 30);

-- Unos u tabelu `Profesor`
INSERT INTO `mydb`.`Profesor` (`Zvanje`, `Korisnik_idKorisnik`) VALUES
('Docent', 2),
('Vanredni profesor', 6),
('Redovni profesor', 8),
('Redovni profesor', 12),
('Asistent',16),
('Docent', 18),
('Docent', 22),
('Docent', 24),
('Vanredni profesor', 25),
('Vanredni profesor', 27),
('Vanredni profesor', 29);



INSERT INTO `mydb`.`Predmet_Student` (`Predmet_idPredmeta`, `Student_Korisnik_idKorisnik`, `DatumUpisa`) VALUES
(2222, 1, '2024-10-01 08:00:00'),
(2257, 1, '2024-10-01 08:00:00'),
(3412, 1, '2024-10-02 09:15:00'),
(1205, 4, '2024-10-03 10:30:00'),
(1309, 5, '2024-10-04 11:45:00'),
(2103, 7, '2024-10-05 12:00:00'),
(4110, 10, '2024-10-06 13:15:00'),
(3125, 1, '2024-10-07 14:30:00'),
(5107, 3, '2024-10-08 15:45:00'),
(6088, 4, '2024-10-09 16:00:00'),
(3413, 5, '2024-10-10 08:00:00'),
(5108, 7, '2024-10-11 09:15:00'),
(3411, 9, '2024-10-12 10:30:00'),
(2233, 10, '2024-10-13 11:45:00'),
(2223, 11, '2024-10-14 12:00:00'),
(2224, 13, '2024-10-15 13:15:00'),
(2225, 14, '2024-10-16 14:30:00'),
(6201, 17, '2024-10-17 15:45:00'),
(6202, 19, '2024-10-18 08:00:00'),
(6203, 20, '2024-10-19 09:15:00'),
(6204, 21, '2024-10-20 10:30:00'),
(6205, 23, '2024-10-21 11:45:00'),
(1205, 25, '2024-10-22 12:00:00'),
(5107, 26, '2024-10-23 13:15:00'),
(3412, 28, '2024-10-24 14:30:00'),
(3125, 30, '2024-10-25 15:45:00');

INSERT INTO `mydb`.`Predmet_Profesor` (`Profesor_Korisnik_idKorisnik`, `Predmet_idPredmeta`) VALUES
(2, 2222),
(2, 2257),
(6, 3412),
(8, 3412),
(2, 1205),
(2, 1309),
(8, 2103),
(6, 4110),
(2, 3125),
(16, 5107),
(16, 3413),
(18, 3411),
(18, 2225),
(2, 2225),
(25, 6203),
(18, 2233),
(6, 2233),
(8, 2233),
(2, 2233),
(2, 6088),   
(2, 6201),
(24, 6201),
(27, 6202),
(29, 6203),
(29, 6204),
(25, 6205);

INSERT INTO `mydb`.`DomaciZadatak` 
(`Naziv`, `Opis`, `Rok`, `Predmet_idPredmeta`, `Profesor_Korisnik_idKorisnik`, `idDomaciZadatak`) 
VALUES
('Zadatak 1', 'Napisati program u Javi', '2024-11-01 23:59:59', 2222, 2, 'DJAVA'),
('Zadatak 2', 'Implementirati algoritam sortiranja', '2024-11-03 23:59:59', 2222, 2, 'DJAVA2'),
('Zadatak 3', 'Razviti jednostavnu web aplikaciju', '2024-11-05 23:59:59', 2222, 2, 'DJAVA3'),
('Zadatak 4', 'Modelovati bazu podataka za biblioteku', '2024-11-10 23:59:59', 2257, 6, 'BAZE1'),
('Zadatak 5', 'Normalizovati date tabele', '2024-11-12 23:59:59', 2257, 6, 'BAZE2'),
('Zadatak 6', 'Napisati REST API servis', '2024-11-14 23:59:59', 2222, 6, 'DJAVA4'),
('Zadatak 7', 'Prvi zadatak iz baze podataka', '2024-11-15 23:59:59', 2257, 8, 'PRVI');

-- Unos u tabelu `Ispit`
INSERT INTO `mydb`.`Ispit` (`DatumIspita`, `Predmet_idPredmeta`) VALUES
('2024-12-01 10:00:00', 2222),
('2024-12-05 10:00:00', 2257),
('2024-12-10 09:00:00', 3412),
('2024-12-12 11:30:00', 1205),
('2024-12-15 14:00:00', 1309),
('2024-12-18 08:00:00', 2103),
('2024-12-20 10:30:00', 4110),
('2024-12-22 12:45:00', 3125),
('2024-12-25 13:15:00', 5107),
('2024-12-28 15:00:00', 6088);

-- Unos u tabelu `DomaciZadatak_Student`
INSERT INTO `mydb`.`DomaciZadatak_Student` (`Bodovi`, `DomaciZadatak_IdDomaciZadatak`, `Student_Korisnik_idKorisnik`) VALUES
(8.5, 'DJAVA', 1),
(7.0, 'BAZE1', 3),
(9.2, 'BAZE2', 4),
(6.8, 'BAZE2', 5),
(8.9, 'DJAVA2', 7),
(7.5, 'DJAVA3', 10);


INSERT INTO `mydb`.`Prisustvo` (`Datum`, `Status`, `Student_Korisnik_idKorisnik`, `Predmet_idPredmeta`) VALUES
('2024-10-01 08:00:00', 'prisutan', 1, 2222),
('2024-10-02 08:00:00', 'prisutan', 1, 2257),
('2024-10-03 09:15:00', 'prisutan', 3, 3412),
('2024-10-04 10:30:00', 'prisutan', 4, 1205),
('2024-10-05 11:45:00', 'prisutan', 5, 1309),
('2024-10-06 12:00:00', 'prisutan', 7, 2103),
('2024-10-07 13:15:00', 'prisutan', 10, 4110),
('2024-10-08 14:30:00', 'prisutan', 1, 3125),
('2024-10-09 15:45:00', 'prisutan', 3, 5107),
('2024-10-10 16:00:00', 'prisutan', 4, 6088);

INSERT INTO `mydb`.`Ispit_Student` (`Student_Korisnik_idKorisnik`,`Bodovi`, `Ocjena`, `Predmet_idPredmeta`, `Ispit_DatumIspita`) VALUES
(1, 85, 9, 2222, '2024-12-01 10:00:00'),
(1, 75, 8,  2257, '2024-12-05 10:00:00'),
(3, 90, 9, 3412, '2024-12-10 09:00:00'),
(4, 88, 9, 1205, '2024-12-12 11:30:00'),
(5, 70, 7, 1309, '2024-12-15 14:00:00'),
(7, 60, 6 ,2103, '2024-12-18 08:00:00'),
(10, 95, 10, 4110, '2024-12-20 10:30:00'),
(1, 77, 8, 3125, '2024-12-22 12:45:00'),
(3, 82, 9, 5107, '2024-12-25 13:15:00'),
(4, 79, 8, 6088, '2024-12-28 15:00:00');


