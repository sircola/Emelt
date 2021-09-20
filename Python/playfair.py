#!/usr/bin/python3
# -*- coding: utf-8 -*-
# To change this license header, choose License Headers in Project Properties.
# To change this template file, choose Tools | Templates
# and open the template in the editor.

__author__ = "Kirschner Bernát"
__date__ = "$2021.09.05. 12:45:19$"

#  nagy betüs lesz, q x-re lesz cserélve, speckó kari nuku
# kodolando = "Ciao q Baby!!1"
# kodolando = "Kwisatz Haderach"
kodolando = "reboot Watt wall week Common Greetings"

# 5 x 5 mátrix Q nincs benne
kodolo = ["PLAMF", "BCDGH", "TUVIX", "KNOWS", "JERZY"]


def sorindex(a):
	i = 0
	for s in kodolo:
		if s.find(a) != -1:
                	return i
		i += 1
	return -1

def oszlopindex(a):
	for s in kodolo:
		i = s.find(a)
		if i != -1:
			return i
	return -1

def kodolazonossorban(sor, oszlop1, oszlop2):
	kodoltOszlopIndex1 = oszlop1 + 1
	if kodoltOszlopIndex1 == 5:
        	kodoltOszlopIndex1 = 0
	kodoltKarakter1 = kodolo[sor][kodoltOszlopIndex1]
	kodoltOszlopIndex2 = oszlop2 + 1
	if kodoltOszlopIndex2 == 5:
		kodoltOszlopIndex2 = 0
	kodoltKarakter2 = kodolo[sor][kodoltOszlopIndex2]
	return kodoltKarakter1 + kodoltKarakter2


def kodolazonososzlopban(oszlop, sor1, sor2):
	kodoltSorIndex1 = sor1 + 1
	if kodoltSorIndex1 == 5:
		kodoltSorIndex1 = 0
	kodoltKarakter1 = kodolo[kodoltSorIndex1][oszlop]
	kodoltSorIndex2 = sor2 + 1
	if kodoltSorIndex2 == 5:
		kodoltSorIndex2 = 0
	kodoltKarakter2 = kodolo[kodoltSorIndex2][oszlop]
	return kodoltKarakter1 + kodoltKarakter2

def kodolteglalapalak(sor1, oszlop1, sor2, oszlop2):
	kodoltKarakter1 = kodolo[sor1][oszlop2]
	kodoltKarakter2 = kodolo[sor2][oszlop1]
	return kodoltKarakter1 + kodoltKarakter2

def kodolbetupar(betupar):
	Betu1 = betupar[0]
	Betu2 = betupar[1]
	sorBetu1 = sorindex(Betu1)
	sorBetu2 = sorindex(Betu2)
	oszlopBetu1 = oszlopindex(Betu1)
	oszlopBetu2 = oszlopindex(Betu2)
	if sorBetu1 == sorBetu2:
		return kodolazonossorban(sorBetu1, oszlopBetu1, oszlopBetu2)
	if oszlopBetu1 == oszlopBetu2:
		return kodolazonososzlopban(oszlopBetu1, sorBetu1, sorBetu2)
	return kodolteglalapalak(sorBetu1, oszlopBetu1, sorBetu2, oszlopBetu2)

def titkosit(szoveg):
	# csak angol ABC, no szokoz
	s = ''.join(filter( lambda x: x in 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', szoveg.upper() ))

	# Q betu kimarad
	s = s.replace('Q','X')

	# dupla karik szetszedése
	i=0
	while i<len(s):
		if i<len(s)-1:
			if s[i]==s[i+1]:
				s=s[:i+1]+'X'+s[i+1:]
		i+=1

        # Z betu a végére ha páratlan
	if len(s) % 2 != 0:
        	s += 'Z'

        # elokészitve
	print('elokeszitve: ',s)

	j = ''
	for i in range(0,len(s),2):
		j += kodolbetupar(s[i:i+2])

	return j

def dekodolazonossorban(sor, oszlop1, oszlop2):
	kodoltOszlopIndex1 = oszlop1 - 1
	if kodoltOszlopIndex1 == -1:
        	kodoltOszlopIndex1 = 4
	kodoltKarakter1 = kodolo[sor][kodoltOszlopIndex1]
	kodoltOszlopIndex2 = oszlop2 - 1
	if kodoltOszlopIndex2 == -1:
		kodoltOszlopIndex2 = 4
	kodoltKarakter2 = kodolo[sor][kodoltOszlopIndex2]
	return kodoltKarakter1 + kodoltKarakter2

def dekodolazonososzlopban(oszlop, sor1, sor2):
	kodoltSorIndex1 = sor1 - 1
	if kodoltSorIndex1 == -1:
		kodoltSorIndex1 = 4
	kodoltKarakter1 = kodolo[kodoltSorIndex1][oszlop]
	kodoltSorIndex2 = sor2 - 1
	if kodoltSorIndex2 == -1:
		kodoltSorIndex2 = 4
	kodoltKarakter2 = kodolo[kodoltSorIndex2][oszlop]
	return kodoltKarakter1 + kodoltKarakter2

def dekodolbetupar(betupar):
	Betu1 = betupar[0]
	Betu2 = betupar[1]
	sorBetu1 = sorindex(Betu1)
	sorBetu2 = sorindex(Betu2)
	oszlopBetu1 = oszlopindex(Betu1)
	oszlopBetu2 = oszlopindex(Betu2)
	if sorBetu1 == sorBetu2:
		return dekodolazonossorban(sorBetu1, oszlopBetu1, oszlopBetu2)
	if oszlopBetu1 == oszlopBetu2:
		return dekodolazonososzlopban(oszlopBetu1, sorBetu1, sorBetu2)
	return kodolteglalapalak(sorBetu1, oszlopBetu1, sorBetu2, oszlopBetu2)


def megfejt(szoveg):

	j = ''
	for i in range(0,len(szoveg),2):
		j += dekodolbetupar(szoveg[i:i+2])

	# X vagy dupla kari vagy Q betü
	i = 0
	while i<len(j):
		if j[i] == 'X':
			if i>0 and i<len(j)-1 and j[i-1]==j[i+1]:
                		j = j[:i] + j[i+1:]
			else:
				j = j[:i] + 'Q' + j[i+1:]
		i+=1

        # Z betut a végéröl leszedjük
	if(j[-1:]=='Z'):
		j = j[:-1]

	return j

if __name__ == "__main__":
	print('szöveg: ',kodolando)
	titok = titkosit(kodolando)
	print('titkositva: ',titok)
	print('megfejtve: ',megfejt(titok))
