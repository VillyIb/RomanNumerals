RomanNumerals/_Readme.txt

Project Roman Numerals

Service prividing parsing and calculation on Roman Numerals.

Author: Villy Ib Jørgensen

Startdate: 2016-12-27

Status: Work in progress



Why not using static method for parsing roman numberic? E.g.: "pubic static bool TryParse(string s, out int result)".

* Main reason is testability, a proper test reference an interface. A static method cannot implement an interface.

