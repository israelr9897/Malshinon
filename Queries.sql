
-- Create a people table - 

CREATE TABLE peoples
(
    id int PRIMARY KEY AUTO_INCREMENT,
    firstName varchar(15),
    lastName varchar(15),
    codeName varchar(15) UNIQUE,
    type ENUM("reporter", "target", "both", "potential_agent"),
    num_reports INT default 0,
    num_mentions INT default 0
)   

-- Create a intelReports table - 

CREATE TABLE intelReports
(
    id INT PRIMARY KEY AUTO_INCREMENT,
    malshinId INT,
    targetId INT,
    stemptime DATETIME DEFAULT NOW(),
    reportText TEXT,
    FOREIGN KEY (malshinId) REFERENCES peoples(id),
    FOREIGN KEY (targetId) REFERENCES peoples(id)
)

-- Create a intelReports table -

CREATE TABLE alerts
 (
 id INT PRIMARY KEY AUTO_INCREMENT,
 targetId INT,
 created_at DATETIME DEFAULT NOW(),
 reason TEXT
 FOREIGN KEY (targetId) REFERENCES peoples(id)
)    

    

