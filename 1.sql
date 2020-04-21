-- 创建数据库
CREATE DATABASE zuoye
ON PRIMARY
(
	NAME='zuoye',
	FILENAME='F:\培训\C#\基础\2020年02月份\0311学员管理系统\作业\数据库\zuoye.mdf',
	SIZE=6MB,
	FILEGROWTH=10%
)
LOG ON
(
	NAME='zuoye_log',
	FILENAME='F:\培训\C#\基础\2020年02月份\0311学员管理系统\作业\数据库\zuoye_log.ldf',
	SIZE=3MB,
	FILEGROWTH=10%
)
GO

USE zuoye
GO

-- 科目
create table Course
(
	CId varchar(20) not null,
	Cname varchar(20) not null ,	
	TId INT not null 

)
-- 教师表
CREATE TABLE Teacher
(
	TId varchar(20) not null,
	Tname varchar(20) not null,
	
)
-- 学生表
CREATE TABLE Student
(
	SId varchar(20) NOT NULL,-- 学号
	Sname  varchar(20) NOT NULL,	-- 姓名
	Sage  datetime not null,	--出生日期
	Ssex   char(2) not null CHECK(Ssex='男' OR Ssex='女'),-- 性别
	
	
)

-- 成绩表
CREATE TABLE SC
(
	SId  varchar(20) NOT NULL,
	CId  varchar(20) NOT NULL,-- 学号
	score int null check(score<=100 and score>=0) ,
	
)


insert into Student values('01' , '赵雷' , '1990-01-01' , '男');
insert into Student values('02' , '钱电' , '1990-12-21' , '男');
insert into Student values('03' , '孙风' , '1990-12-20' , '男');
insert into Student values('04' , '李云' , '1990-08-06' , '男');
insert into Student values('05' , '周梅' , '1991-12-01' , '女');
insert into Student values('06' , '吴兰' , '1992-01-01' , '女');
insert into Student values('07' , '郑竹' , '1989-01-01' , '女');
insert into Student values('08' , '王菊' , '1990-01-20' , '女');
insert into Student values('09' , '张三' , '2017-12-20' , '女');
insert into Student values('10' , '李四' , '2017-12-15' , '女');
insert into Student values('11' , '李四' , '2012-06-06' , '女');
insert into Student values('12' , '赵六' , '2013-06-13' , '女');
insert into Student values('13' , '孙七' , '2014-06-01' , '女');


insert into Course values('01' , '语文' , '02');
insert into Course values('02' , '数学' , '01');
insert into Course values('03' , '英语' , '03');

insert into Teacher values('01' , '张三');
insert into Teacher values('02' , '李四');
insert into Teacher values('03' , '王五');

insert into SC values('01' , '01' , 80);
insert into SC values('01' , '02' , 90);
insert into SC values('01' , '03' , 99);
insert into SC values('02' , '01' , 70);
insert into SC values('02' , '02' , 60);
insert into SC values('02' , '03' , 80);
insert into SC values('03' , '01' , 80);
insert into SC values('03' , '02' , 80);
insert into SC values('03' , '03' , 80);
insert into SC values('04' , '01' , 50);
insert into SC values('04' , '02' , 30);
insert into SC values('04' , '03' , 20);
insert into SC values('05' , '01' , 76);
insert into SC values('05' , '02' , 87);
insert into SC values('06' , '01' , 31);
insert into SC values('06' , '03' , 34);
insert into SC values('07' , '02' , 89);
insert into SC values('07' , '03' , 98);

SELECT * FROM Student
SELECT * FROM SC
SELECT * FROM Course
SELECT * FROM Teacher


--1 查询" 01 "课程比" 02 "课程成绩高的学生的信息及课程分数
SELECT * FROM student RIGHT JOIN  (SELECT a.SId,a.score clASs1,b.score clASs2 FROM (SELECT * FROM sc WHERE sc.CId='01')AS a,(SELECT * FROM sc WHERE sc.CId='02')AS b WHERE a.SId=b.SId AND a.score>b.score)r ON student.SId=r.SId

--2.查询同时存在" 01 "课程和" 02 "课程的情况

SELECT a.* , b.score 课程01的分数 ,c.score 课程02的分数 FROM Student a , SC b , SC c WHERE a.SID = b.SID AND a.SID = c.SID AND b.CID = '01' AND c.CID = '02' AND b.score < c.score

--3 查询存在" 01 "课程但可能不存在" 02 "课程的情况(不存在时显示为 null )
SELECT a.* , b.score 课程01的分数 ,c.score 课程02的分数 FROM Student a LEFT JOIN SC b on a.SID = b.SID AND b.CID = '01' LEFT JOIN SC c on a.SID = c.SID AND c.CID = '02' WHERE isnull(b.score,0) < c.score

--4 查询不存在" 01 "课程但存在" 02 "课程的情况
SELECT a.* , b.score 课程01的分数 ,c.score 课程02的分数 FROM Student a LEFT JOIN SC b on a.SID = b.SID AND b.CID = '02' LEFT JOIN SC c on a.SID = c.SID AND c.CID = '01' WHERE isnull(b.score,0) > c.score

--5 查询平均成绩大于等于 60 分的同学的学生编号和学生姓名和平均成绩
SELECT student.sid,student.sname,AVG(sc.score) FROM student,sc WHERE student.sid=sc.sid GROUP BY student.sid,student.sname HAVING AVG(sc.score)>=60 

--6 查询在 SC 表存在成绩的学生信息
SELECT a.sid, a.sname,a.sage,a.Ssex FROM student a JOIN (SELECT DISTINCT SId FROM SC WHERE score<>0)b ON a.sid = b.sid

--7 查询所有同学的学生编号、学生姓名、选课总数、所有课程的总成绩(没成绩的显示为 null )
SELECT a.SID 学生编号 , a.Sname 学生姓名 , COUNT(b.CID) 选课总数, SUM(score) 所有课程的总成绩 FROM Student a , SC b WHERE a.SID = b.SID GROUP BY a.SID,a.Sname order BY a.SID

--8 查有成绩的学生信息
SELECT * FROM student WHERE exists (SELECT sc.sid FROM sc WHERE student.sid = sc.sid);

--9 查询「李」姓老师的数量
SELECT COUNT(Tname) FROM teacher WHERE Tname LIKE '李%';

--10 查询学过「张三」老师授课的同学的信息
SELECT m.* FROM Student m WHERE SID  in (SELECT distinct SC.SID FROM SC , Course , Teacher WHERE SC.CID = Course.CID AND Course.TID = Teacher.TID AND Teacher.Tname = '张三') order BY m.SID

--11 查询没有学全所有课程的同学的信息
SELECT Student.* FROM Student , SC WHERE Student.SID = SC.SID GROUP BY Student.SID , Student.Sname , Student.Sage , Student.Ssex HAVING COUNT(CID) < (SELECT COUNT(CID) FROM Course)

--12 查询至少有一门课与学号为" 01 "的同学所学相同的同学的信息
SELECT distinct Student.* FROM Student , SC WHERE Student.SID = SC.SID AND SC.CID in (SELECT CID FROM SC WHERE SID = '01') AND Student.SID <> '01'

--13 查询和" 01 "号的同学学习的课程 完全相同的其他同学的信息
SELECT Student.* FROM Student WHERE SID in (SELECT distinct SC.SID FROM SC WHERE SID <> '01' AND SC.CID in (SELECT distinct CID FROM SC WHERE SID = '01') GROUP BY SC.SID HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE SID='01'))SELECT Student.* FROM Student WHERE SID in
(SELECT distinct SC.SID FROM SC WHERE SID <> '01' AND SC.CID in (SELECT distinct CID FROM SC WHERE SID = '01') GROUP BY SC.SID HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE SID='01'))

--14 查询没学过"张三"老师讲授的任一门课程的学生姓名
SELECT student.* FROM student WHERE student.SID not in (SELECT distinct sc.SID FROM sc , course , teacher WHERE sc.CID = course.CID AND course.TID = teacher.TID AND teacher.tname = '张三') order BY student.SID

--15 查询两门及其以上不及格课程的同学的学号，姓名及其平均成绩
SELECT student.SID , student.sname , CAST(AVG(score) AS decimal(18,2)) AVG_score FROM student , sc WHERE student.SID = SC.SID AND student.SID in (SELECT SID FROM SC WHERE score < 60 GROUP BY SID HAVING COUNT(1) >= 2) GROUP BY student.SID , student.sname

--16 检索" 01 "课程分数小于 60，按分数降序排列的学生信息
SELECT student.* , sc.CID , sc.score FROM student , sc WHERE student.SID = SC.SID AND sc.score < 60 AND sc.CID = '01' order BY sc.score desc

--17 按平均成绩从高到低显示所有学生的所有课程的成绩以及平均成绩
SELECT a.SID 学生编号 , a.Sname 学生姓名 ,
MAX(cASe c.Cname when '语文' THEN b.score ELSE null end) 语文 ,
MAX(cASe c.Cname when '数学' THEN b.score ELSE null end) 数学 ,
MAX(cASe c.Cname when '英语' THEN b.score ELSE null end) 英语 ,
CAST(AVG(b.score) AS decimal(18,2)) 平均分 FROM Student a LEFT JOIN SC b on a.SID = b.SID LEFT JOIN Course c on b.CID = c.CID GROUP BY a.SID , a.Sname order BY 平均分 desc

--18 查询各科成绩最高分、最低分和平均分：
--以如下形式显示：课程 ID，课程 name，最高分，最低分，平均分，及格率，中等率，优良率，优秀率
--及格为>=60，中等为：70-80，优良为：80-90，优秀为：>=90
--要求输出课程号和选修人数，查询结果按人数降序排列，若人数相同，按课程号升序排列
SELECT m.CID 课程编号 , m.Cname 课程名称 ,
(SELECT MAX(score) FROM SC WHERE CID = m.CID) 最高分 ,
(SELECT min(score) FROM SC WHERE CID = m.CID) 最低分 ,
(SELECT CAST(AVG(score) AS decimal(18,2)) FROM SC WHERE CID = m.CID) 平均分 ,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 60)*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) 及格率,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 70 AND score < 80 )*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) 中等率 ,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 80 AND score < 90 )*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) 优良率 ,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 90)*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) 优秀率
FROM Course m order BY m.CID

--19 按各科成绩进行排序，并显示排名， Score 重复时保留名次空缺
SELECT t.* , px = (SELECT COUNT(1) FROM SC WHERE CID = t.CID AND score > t.score) + 1 FROM sc t order BY t.cid , px

--20 按各科成绩进行排序，并显示排名， Score 重复时合并名次
SELECT t.* , px = (SELECT COUNT(distinct score) FROM SC WHERE CID = t.CID AND score >= t.score) FROM sc t order BY t.cid , px

--21 查询学生的总成绩，并进行排名，总分重复时保留名次空缺
SELECT t.* , px = rank() over(order BY 总成绩 desc) FROM (SELECT m.SID 学生编号 ,m.Sname 学生姓名 ,isnull(SUM(score),0) 总成绩 FROM Student m LEFT JOIN SC n on m.SID = n.SID GROUP BY m.SID , m.Sname) t order BY px
--22 查询学生的总成绩，并进行排名，总分重复时不保留名次空缺
SELECT t.* , px = DENSE_RANK() over(order BY 总成绩 desc) FROM (SELECT m.SID 学生编号 ,m.Sname 学生姓名 ,isnull(SUM(score),0) 总成绩 FROM Student m LEFT JOIN SC n on m.SID = n.SID GROUP BY m.SID , m.Sname) t order BY px

--23 统计各科成绩各分数段人数：课程编号，课程名称，[100-85]，[85-70]，[70-60]，[60-0] 及所占百分比
SELECT course.cname, course.cid,
SUM(cASe when sc.score<=100 AND sc.score>85 THEN 1 ELSE 0 end) AS "[100-85]",
SUM(cASe when sc.score<=85 AND sc.score>70 THEN 1 ELSE 0 end) AS "[85-70]",
SUM(cASe when sc.score<=70 AND sc.score>60 THEN 1 ELSE 0 end) AS "[70-60]",
SUM(cASe when sc.score<=60 AND sc.score>0 THEN 1 ELSE 0 end) AS "[60-0]"
FROM sc LEFT JOIN course
on sc.cid = course.cid
GROUP BY sc.cid,course.cname,course.cid;

--24 查询各科成绩前三名的记录
SELECT m.* , n.CID , n.score FROM Student m, SC n WHERE m.SID = n.SID AND n.score in (SELECT top 3 score FROM sc WHERE CID = n.CID order BY score desc) order BY n.CID , n.score desc

--25 查询每门课程被选修的学生数
SELECT Cid , COUNT(SID) 学生数 FROM sc GROUP BY CID

--26 查询出只选修两门课程的学生学号和姓名
SELECT Student.SID , Student.Sname FROM Student , SC WHERE Student.SID = SC.SID GROUP BY Student.SID , Student.Sname HAVING COUNT(SC.CID) = 2 order BY Student.SID

--27 查询男生、女生人数
SELECT COUNT(Ssex) AS 男生人数 FROM Student WHERE Ssex = N'男'
SELECT COUNT(Ssex) AS 女生人数 FROM Student WHERE Ssex = N'女'

--28 查询名字中含有「风」字的学生信息
SELECT * FROM student WHERE sname LIKE N'%风%'


--29 查询同名同性学生名单，并统计同名人数
SELECT Sname 学生姓名 , COUNT(*) 人数 FROM Student GROUP BY Sname HAVING COUNT(*) > 1

--30 查询 1990 年出生的学生名单

SELECT * FROM Student WHERE CONVERT(VARCHAR(4),sage,120) = '1990'

--31 查询每门课程的平均成绩，结果按平均成绩降序排列，平均成绩相同时，按课程编号升序排列
SELECT m.CID , m.Cname , cast(avg(n.score) as decimal(18,2)) avg_score FROM Course m, SC n WHERE m.CID = n.CID GROUP BY m.CID , m.Cname ORDER BY avg_score DESC, m.CID asc

--32 查询平均成绩大于等于 85 的所有学生的学号、姓名和平均成绩
SELECT a.SID , a.Sname , cast(avg(b.score) as decimal(18,2)) avg_score FROM Student a , sc b WHERE a.SID = b.SID GROUP BY a.SID , a.Sname having cast(avg(b.score) as decimal(18,2)) >= 85 ORDER BY a.SID

--33 查询课程名称为「数学」，且分数低于 60 的学生姓名和分数
SELECT sname , score FROM Student , SC , Course WHERE SC.SID = Student.SID and SC.CID = Course.CID and Course.Cname = N'数学' and score < 60

--34 查询所有学生的课程及分数情况（存在学生没成绩，没选课的情况）
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID ORDER BY Student.SID , SC.CID

--35 查询任何一门课程成绩在 70 分以上的姓名、课程名称和分数
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score >= 70 ORDER BY Student.SID , SC.CID

--36 查询不及格的课程
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score < 60 ORDER BY Student.SID , SC.CID

--37 查询课程编号为 01 且课程成绩在 80 分以上的学生的学号和姓名
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.CID = '01' and SC.score >= 80 ORDER BY Student.SID , SC.CID

--38 求每门课程的学生人数
SELECT Course.CID , Course.Cname , count(*) 学生人数 FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname ORDER BY Course.CID , Course.Cname

--39 成绩不重复，查询选修「张三」老师所授课程的学生中，成绩最高的学生信息及其成绩

SELECT top 1 Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course , Teacher WHERE Student.SID = SC.SID and SC.CID = Course.CID and Course.TID = Teacher.TID and Teacher.Tname = N'张三' ORDER BY SC.score DESC

--40 成绩有重复的情况下，查询选修「张三」老师所授课程的学生中，成绩最高的学生信息及其成绩
SELECT student.*, sc.score, sc.cid FROM student, teacher, course,sc 
WHERE teacher.tid = course.tid
AND sc.sid = student.sid
AND sc.cid = course.cid
AND teacher.tname = '张三'
AND sc.score = (
    SELECT Max(sc.score) 
    FROM sc,student, teacher, course
    WHERE teacher.tid = course.tid
    AND sc.sid = student.sid
    AND sc.cid = course.cid
    AND teacher.tname = '张三'
);

--41 查询不同课程成绩相同的学生的学生编号、课程编号、学生成绩
SELECT m.* FROM SC m ,(SELECT CID , score FROM SC GROUP BY CID , score having count(1) > 1) n WHERE m.CID= n.CID and m.score = n.score ORDER BY m.CID , m.score , m.SID

--42 查询每门功成绩最好的前两名
SELECT t.* FROM sc t WHERE score in (SELECT top 2 score FROM sc WHERE CID = T.CID ORDER BY score DESC) ORDER BY t.CID , t.score DESC

--43 统计每门课程的学生选修人数（超过 5 人的课程才统计）。
SELECT Course.CID , Course.Cname , count(*) 学生人数 FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname having count(*) >= 5 ORDER BY 学生人数 DESC , Course.CID

--44 检索至少选修两门课程的学生学号
SELECT student.SID , student.Sname FROM student , SC WHERE student.SID = SC.SID GROUP BY student.SID , student.Sname having count(1) >= 2 ORDER BY student.SID

--45 查询选修了全部课程的学生信息
SELECT student.* FROM student WHERE SID in(SELECT SID FROM sc GROUP BY SID HAVING count(1) = (SELECT count(1) FROM course))

--46 查询各学生的年龄，只按年份来算
SELECT * , DATEDIFF(yy , sage , GETDATE()) 年龄 FROM student

--47 按照出生日期来算，当前月日 < 出生年月的月日则，年龄减一
SELECT * , case when right(convert(VARCHAR(10),GETDATE(),120),5) < right(convert(VARCHAR(10),sage,120),5) then DATEDIFF(yy , sage , GETDATE()) - 1 else DATEDIFF(yy , sage , GETDATE()) end 年龄 FROM student

--48 查询本周过生日的学生
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0

--49 查询下周过生日的学生
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1

--50 查询本月过生日的学生
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0

--51 查询下月过生日的学生
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1
