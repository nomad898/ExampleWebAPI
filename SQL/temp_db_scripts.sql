--CREATE TEMP TABLE tmp_a (
--	VALUE INT
--);

--CREATE TEMP TABLE tmp_b (
--	VALUE INT
--);

--INSERT INTO tmp_a (VALUE) VALUES (10);
--INSERT INTO tmp_b (VALUE) VALUES (100);

--DELETE FROM tmp_a;
--DELETE FROM tmp_b;

WITH result_table as (
	SELECT *
	FROM (
		SELECT a.VALUE
		FROM tmp_a a
		UNION ALL
		SELECT b.VALUE
		FROM tmp_b b
		WHERE NOT EXISTS (
			SELECT 1
			FROM tmp_a
		)	
	) tmp
) 
  SELECT *
  FROM result_table 
  UNION ALL
  SELECT NULL
  WHERE NOT EXISTS (
    SELECT 1
	FROM result_table
  );