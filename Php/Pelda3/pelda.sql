/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 * Author:  tanulo1410
 * Created: 2021.10.02.
 */

CREATE DATABASE hirdeteses DEFAULT CHARACTER SET=utf8 COLLATE utf8_hungarian_ci;
CREATE DATABASE hataridonaplo DEFAULT CHARACTER SET=utf8 COLLATE utf8_hungarian_ci;

USE hataridonaplo;

CREATE TABLE elgolaltsagok (
    id INT AUTO_INCREMENT PRIMARY KEY,
    szoveg VARCHAR(40)
);

INSERT INTO elgolaltsagok(szoveg) VALUES
("Megbeszélés"),
("Üzleti ebéd");

CREATE TABLE napi (
    ora INT,
    id INT,
    datum DATE PRIMARY KEY
);

USE hirdeteses;

CREATE TABLE users (
    uid VARCHAR(30)  PRIMARY KEY,
    pwd VARCHAR(40)
);

INSERT INTO users(uid,pwd) VALUES
("bernie",MD5("titok"));

/*
users(uid,pwd)
hirdetes(hid,text,uid)
licitek(hid, datumido, licit, uid)

elfoglaltsagok(id, szoveg)
napi(ora,id,datum*)
heti(napszak,id,datum,hetnapja)
*/