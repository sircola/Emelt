
DROP DATABASE IF EXISTS forma1;

CREATE DATABASE forma1 DEFAULT CHARACTER SET=utf8 COLLATE utf8_hungarian_ci;

CREATE TABLE versenyzok(
	nev VARCHAR(100),
	rajtszam INT PRIMARY KEY,
	nemzetiseg VARCHAR(100),
	ujonc INT(1),
	belepes DATE
);

CREATE TABLE csapatok(
	nev VARCHAR(100),
	azon INT PRIMARY KEY,
	motor VARCHAR(100)
);

CREATE TABLE kapcsolat(
	versenyzoazon INT,
	csapatazon INT,
	FOREIGN KEY(versenyzoazon) REFERENCES versenyzok(rajtszam),
	FOREIGN KEY(csapatazon) REFERENCES csapatok(azon)
);

INSERT INTO csapatok VALUES
('Scuderia Ferrari Mission Winnow', 2, 'Ferrari'),
('Red Bull Racing', 3, 'Honda'),
('McLaren F1 Team', 4, 'Mercedes'),
('Alpine F1 Team', 5, 'Renault'),
('Scuderia Alpha Tauri Honda', 6, 'Honda'),
('Aston Martin Cognization F1 Team', 7, 'Mercedes'),
('Mercedes-AMG Petronas Motorsport', 1, 'Mercedes'),
('Williams Racing', 8, 'Mercedes'),
('Haas F1 Team', 9, 'Ferrari'),
('Alfa Romeo Racing Orlen', 10, 'Ferrari');

INSERT INTO versenyzok VALUES
('Lewis Hamilton', 44, 'brit', 0, '2012-12-01' ),
('Valtteri Bottas', 77, 'finn', 0, '2017-01-08' ),
('Carlos Sainz Jnr', 55, 'spanyol', 0, '2020-12-28' ),
('Charles Leclerc', 16, 'monacói', 0, '2019-01-03' ),
('Max Verstappen', 33, 'holland', 0, '2016-01-01' ),
('Sergio Perez', 11, 'mexikói', 0, '2021-02-03' ),
('Daniel Ricciardo', 3, 'ausztrál', 0, '2021-01-18' ),
('Lando Norris', 4, 'brit', 0, '2019-01-09' ),
('Esteban Ocon', 31, 'francia', 0, '2019-12-02' ),
('Fernando Alonso', 14, 'spanyol', 0, '2021-02-28' ),
('Yuki Tsunoda', 22, 'japán', 1, '2021-02-14' ),
('Pierre Gasly', 10, 'francia', 0, '2019-08-24' ),
('Sebastion Vettel', 5, 'német', 0, '2020-12-30' ),
('Lance Stroll', 18, 'kanadai', 0, '2019-01-05' ),
('Kimi Raikonen', 7, 'finn', 0, '2019-01-16' ),
('Antonio Giovinazzi', 99, 'olasz', 0, '2019-01-16' ),
('Mick Schumaher', 47, 'német', 1, '2021-01-02' ),
('Nikita Mazepin', 9, 'orosz', 1, '2021-01-06' ),
('George Russel ', 63, 'brit', 0, '2019-02-01' ),
('Nicholas Latifi', 6, 'kanadai', 0, '2020-01-07' );

INSERT INTO kapcsolat VALUES
(44, 1),
(77, 1),
(55, 2),
(16, 2),
(33, 3),
(11, 3),
(3, 4),
(4, 4),
(31, 5),
(14, 5),
(22, 6),
(10, 6),
(5, 7),
(18, 7),
(7, 10),
(99, 10),
(47, 9),
(9, 9),
(63, 8),
(6, 8);

DELIMITER $$
CREATE PROCEDURE pr_NemUjoncVersenyzok()
BEGIN
SELECT
	versenyzok.rajtszam AS 'vRajt',
	versenyzok.nev AS 'vNev',
	versenyzok.nemzetiseg AS 'vNemz',
	csapatok.nev AS 'csNev'
FROM
	versenyzok
	LEFT JOIN kapcsolat ON versenyzok.rajtszam=kapcsolat.versenyzoazon
	LEFT JOIN csapatok ON kapcsolat.csapatazon=csapatok.azon
WHERE versenyzok.ujonc <> 1
ORDER BY versenyzok.nev;
END $$
DELIMITER ;


CREATE USER 'root'@'_gateway' identified by '';
GRANT ALL PRIVILEGES ON *.* TO 'root'@'_gateway' WITH GRANT OPTION;
FLUSH PRIVILEGES;


DELIMITER $$
CREATE PROCEDURE pr_Csapatok()
BEGIN
	SELECT nev, azon FROM csapatok;
END $$
DELIMITER ;



DELIMITER $$
CREATE PROCEDURE pr_UjVersenyzo(
	IN vNev VARCHAR(100),
	IN vRajt INT,
	IN csAzon INT,
	IN ujonc INT(1),
	IN belep DATE
)
BEGIN
INSERT INTO versenyzok(nev, rajtszam, ujonc, belepes)
VALUES (vNev, vRajt, ujonc, belep);

INSERT INTO kapcsolat(csapatazon, versenyzoazon)
VALUES (csAzon, vRajt);
END $$
DELIMITER ;



DELIMITER $$
CREATE PROCEDURE pr_CheckRajtszam( IN rajtszam INT)
BEGIN
SELECT
	COUNT(versenyzok.nev) AS 'van'
FROM versenyzok
WHERE versenyzok.rajtszam = rajtszam;
END $$
DELIMITER ;


