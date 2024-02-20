CREATE TABLE metric_leaderboard(
pilot_name VARCHAR(50),
car_name VARCHAR(50),
data_tick BIGINT,
metric_name VARCHAR(100),
metric_value VARCHAR(100),
PRIMARY KEY(pilot_name, car_name, data_tick))