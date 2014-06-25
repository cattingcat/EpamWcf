CREATE TABLE PersonTbl (
    id  INT        NOT NULL,
    name    NCHAR (20) NULL,
    lastname NCHAR (20) NULL,
    dob            DATE       NULL,
    PRIMARY KEY CLUSTERED (id ASC)
);

CREATE TABLE PhoneTbl (
    id       INT        NOT NULL,
    number    NCHAR (50) NULL,
    person_id INT        NULL,
    PRIMARY KEY CLUSTERED (id ASC),
    FOREIGN KEY (person_id) REFERENCES PersonTbl(id)
);


INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (0, N'John', N'Petrov', N'1968-10-12')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (1, N'Rachel', N'Ivanov', N'1993-10-10')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (2, N'Mickhail', N'Sidorov', N'1996-08-11')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (3, N'Ivan', N'Smith', N'1989-03-10')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (4, N'Petr', N'Black', N'1963-11-06')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (5, N'Sergey', N'Blare', N'1976-10-19')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (6, N'Vasiliy', N'Dodson', N'1969-07-07')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (7, N'Andrew', N'Elmers', N'1989-09-05')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (8, N'Bruce', N'Gollson', N'1962-10-10')
INSERT INTO PersonTbl (id, name, lastname, dob) VALUES (9, N'Arnold', N'Qwerty', N'1993-11-18')

INSERT INTO PhoneTbl (id, number, person_id) VALUES (0, N'1b65546c-04b4-442b-b1c8-1b0e1cdd3e5f              ', 0)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (1, N'31957f27-2bfe-41f0-96c0-aafbe7f54892              ', 1)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (2, N'b5a9e629-9d54-48b4-bf23-aa2c4ac0904a              ', 2)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (3, N'1186ab89-f5cf-4208-acc6-01f7802c6cb3              ', 3)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (4, N'951d5ab7-e6b1-4aad-9ede-dde16a1f9112              ', 4)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (5, N'ee2da68b-6167-4a19-af93-85a8d10efb68              ', 5)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (6, N'8a53fe7f-41dd-45b7-93fb-4d920298dad6              ', 6)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (7, N'd12b2b04-7fb9-4e3a-bc6d-6073911bc4b1              ', 7)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (8, N'015ed11d-d75b-4ca5-b4b4-f764c8e11775              ', 8)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (9, N'043bb324-a30c-49e6-9b02-fe47ef5e27d9              ', 9)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (11, N'435643745674576456745765374567456745              ', 6)
INSERT INTO PhoneTbl (id, number, person_id) VALUES (12, N'463678657985789679876967894784578685              ', 7)
