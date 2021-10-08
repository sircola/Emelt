/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 * Author:  tanulo10225
 * Created: 2021.10.05.
 */

 CREATE DATABASE hirdetes DEFAULT CHARACTER SET=utf8 COLLATE utf8_hungarian_ci;

USE hirdetes;


 CREATE TABLE felhasznalok (
    uid INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(512) ,
    jelszo VARCHAR(512),
);

INSERT INTO felhasznalok (email, jelszo) VALUES
('bernie@bernie', sha1("123")),
('gipsz@gipsz', sha1("123")),
('pistike@pistike', sha1("123"));


CREATE TABLE hirdetes (
    hid INT AUTO_INCREMENT PRIMARY KEY,
    text VARCHAR(1000),
    ar INT,
    uid INT,
    FOREIGN KEY (uid) REFERENCES felhasznalok(uid)
);


INSERT INTO hirdetes (text, ar, uid) VALUES
('jhljhlkjhlkjh', 33333, 2),
('Walami', 12121212, 2),
('Használt wécékefe.', 1001, 2),
('Pistike hirdetése', 999, 4),
('Gipsz Jakab hirdetése,\r\nami több soros.', 2021, 3);


CREATE TABLE licitek (
    hid INT,
    datumido DATE,
    licit INT,
    uid INT,
    FOREIGN KEY (uid) REFERENCES felhasznalok(uid),
    FOREIGN KEY (hid) REFERENCES hirdetes(hid)
);

