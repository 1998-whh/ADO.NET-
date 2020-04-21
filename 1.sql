-- �������ݿ�
CREATE DATABASE zuoye
ON PRIMARY
(
	NAME='zuoye',
	FILENAME='F:\��ѵ\C#\����\2020��02�·�\0311ѧԱ����ϵͳ\��ҵ\���ݿ�\zuoye.mdf',
	SIZE=6MB,
	FILEGROWTH=10%
)
LOG ON
(
	NAME='zuoye_log',
	FILENAME='F:\��ѵ\C#\����\2020��02�·�\0311ѧԱ����ϵͳ\��ҵ\���ݿ�\zuoye_log.ldf',
	SIZE=3MB,
	FILEGROWTH=10%
)
GO

USE zuoye
GO

-- ��Ŀ
create table Course
(
	CId varchar(20) not null,
	Cname varchar(20) not null ,	
	TId INT not null 

)
-- ��ʦ��
CREATE TABLE Teacher
(
	TId varchar(20) not null,
	Tname varchar(20) not null,
	
)
-- ѧ����
CREATE TABLE Student
(
	SId varchar(20) NOT NULL,-- ѧ��
	Sname  varchar(20) NOT NULL,	-- ����
	Sage  datetime not null,	--��������
	Ssex   char(2) not null CHECK(Ssex='��' OR Ssex='Ů'),-- �Ա�
	
	
)

-- �ɼ���
CREATE TABLE SC
(
	SId  varchar(20) NOT NULL,
	CId  varchar(20) NOT NULL,-- ѧ��
	score int null check(score<=100 and score>=0) ,
	
)


insert into Student values('01' , '����' , '1990-01-01' , '��');
insert into Student values('02' , 'Ǯ��' , '1990-12-21' , '��');
insert into Student values('03' , '���' , '1990-12-20' , '��');
insert into Student values('04' , '����' , '1990-08-06' , '��');
insert into Student values('05' , '��÷' , '1991-12-01' , 'Ů');
insert into Student values('06' , '����' , '1992-01-01' , 'Ů');
insert into Student values('07' , '֣��' , '1989-01-01' , 'Ů');
insert into Student values('08' , '����' , '1990-01-20' , 'Ů');
insert into Student values('09' , '����' , '2017-12-20' , 'Ů');
insert into Student values('10' , '����' , '2017-12-15' , 'Ů');
insert into Student values('11' , '����' , '2012-06-06' , 'Ů');
insert into Student values('12' , '����' , '2013-06-13' , 'Ů');
insert into Student values('13' , '����' , '2014-06-01' , 'Ů');


insert into Course values('01' , '����' , '02');
insert into Course values('02' , '��ѧ' , '01');
insert into Course values('03' , 'Ӣ��' , '03');

insert into Teacher values('01' , '����');
insert into Teacher values('02' , '����');
insert into Teacher values('03' , '����');

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


--1 ��ѯ" 01 "�γ̱�" 02 "�γ̳ɼ��ߵ�ѧ������Ϣ���γ̷���
SELECT * FROM student RIGHT JOIN  (SELECT a.SId,a.score clASs1,b.score clASs2 FROM (SELECT * FROM sc WHERE sc.CId='01')AS a,(SELECT * FROM sc WHERE sc.CId='02')AS b WHERE a.SId=b.SId AND a.score>b.score)r ON student.SId=r.SId

--2.��ѯͬʱ����" 01 "�γ̺�" 02 "�γ̵����

SELECT a.* , b.score �γ�01�ķ��� ,c.score �γ�02�ķ��� FROM Student a , SC b , SC c WHERE a.SID = b.SID AND a.SID = c.SID AND b.CID = '01' AND c.CID = '02' AND b.score < c.score

--3 ��ѯ����" 01 "�γ̵����ܲ�����" 02 "�γ̵����(������ʱ��ʾΪ null )
SELECT a.* , b.score �γ�01�ķ��� ,c.score �γ�02�ķ��� FROM Student a LEFT JOIN SC b on a.SID = b.SID AND b.CID = '01' LEFT JOIN SC c on a.SID = c.SID AND c.CID = '02' WHERE isnull(b.score,0) < c.score

--4 ��ѯ������" 01 "�γ̵�����" 02 "�γ̵����
SELECT a.* , b.score �γ�01�ķ��� ,c.score �γ�02�ķ��� FROM Student a LEFT JOIN SC b on a.SID = b.SID AND b.CID = '02' LEFT JOIN SC c on a.SID = c.SID AND c.CID = '01' WHERE isnull(b.score,0) > c.score

--5 ��ѯƽ���ɼ����ڵ��� 60 �ֵ�ͬѧ��ѧ����ź�ѧ��������ƽ���ɼ�
SELECT student.sid,student.sname,AVG(sc.score) FROM student,sc WHERE student.sid=sc.sid GROUP BY student.sid,student.sname HAVING AVG(sc.score)>=60 

--6 ��ѯ�� SC ����ڳɼ���ѧ����Ϣ
SELECT a.sid, a.sname,a.sage,a.Ssex FROM student a JOIN (SELECT DISTINCT SId FROM SC WHERE score<>0)b ON a.sid = b.sid

--7 ��ѯ����ͬѧ��ѧ����š�ѧ��������ѡ�����������пγ̵��ܳɼ�(û�ɼ�����ʾΪ null )
SELECT a.SID ѧ����� , a.Sname ѧ������ , COUNT(b.CID) ѡ������, SUM(score) ���пγ̵��ܳɼ� FROM Student a , SC b WHERE a.SID = b.SID GROUP BY a.SID,a.Sname order BY a.SID

--8 ���гɼ���ѧ����Ϣ
SELECT * FROM student WHERE exists (SELECT sc.sid FROM sc WHERE student.sid = sc.sid);

--9 ��ѯ�������ʦ������
SELECT COUNT(Tname) FROM teacher WHERE Tname LIKE '��%';

--10 ��ѯѧ������������ʦ�ڿε�ͬѧ����Ϣ
SELECT m.* FROM Student m WHERE SID  in (SELECT distinct SC.SID FROM SC , Course , Teacher WHERE SC.CID = Course.CID AND Course.TID = Teacher.TID AND Teacher.Tname = '����') order BY m.SID

--11 ��ѯû��ѧȫ���пγ̵�ͬѧ����Ϣ
SELECT Student.* FROM Student , SC WHERE Student.SID = SC.SID GROUP BY Student.SID , Student.Sname , Student.Sage , Student.Ssex HAVING COUNT(CID) < (SELECT COUNT(CID) FROM Course)

--12 ��ѯ������һ�ſ���ѧ��Ϊ" 01 "��ͬѧ��ѧ��ͬ��ͬѧ����Ϣ
SELECT distinct Student.* FROM Student , SC WHERE Student.SID = SC.SID AND SC.CID in (SELECT CID FROM SC WHERE SID = '01') AND Student.SID <> '01'

--13 ��ѯ��" 01 "�ŵ�ͬѧѧϰ�Ŀγ� ��ȫ��ͬ������ͬѧ����Ϣ
SELECT Student.* FROM Student WHERE SID in (SELECT distinct SC.SID FROM SC WHERE SID <> '01' AND SC.CID in (SELECT distinct CID FROM SC WHERE SID = '01') GROUP BY SC.SID HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE SID='01'))SELECT Student.* FROM Student WHERE SID in
(SELECT distinct SC.SID FROM SC WHERE SID <> '01' AND SC.CID in (SELECT distinct CID FROM SC WHERE SID = '01') GROUP BY SC.SID HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE SID='01'))

--14 ��ѯûѧ��"����"��ʦ���ڵ���һ�ſγ̵�ѧ������
SELECT student.* FROM student WHERE student.SID not in (SELECT distinct sc.SID FROM sc , course , teacher WHERE sc.CID = course.CID AND course.TID = teacher.TID AND teacher.tname = '����') order BY student.SID

--15 ��ѯ���ż������ϲ�����γ̵�ͬѧ��ѧ�ţ���������ƽ���ɼ�
SELECT student.SID , student.sname , CAST(AVG(score) AS decimal(18,2)) AVG_score FROM student , sc WHERE student.SID = SC.SID AND student.SID in (SELECT SID FROM SC WHERE score < 60 GROUP BY SID HAVING COUNT(1) >= 2) GROUP BY student.SID , student.sname

--16 ����" 01 "�γ̷���С�� 60���������������е�ѧ����Ϣ
SELECT student.* , sc.CID , sc.score FROM student , sc WHERE student.SID = SC.SID AND sc.score < 60 AND sc.CID = '01' order BY sc.score desc

--17 ��ƽ���ɼ��Ӹߵ�����ʾ����ѧ�������пγ̵ĳɼ��Լ�ƽ���ɼ�
SELECT a.SID ѧ����� , a.Sname ѧ������ ,
MAX(cASe c.Cname when '����' THEN b.score ELSE null end) ���� ,
MAX(cASe c.Cname when '��ѧ' THEN b.score ELSE null end) ��ѧ ,
MAX(cASe c.Cname when 'Ӣ��' THEN b.score ELSE null end) Ӣ�� ,
CAST(AVG(b.score) AS decimal(18,2)) ƽ���� FROM Student a LEFT JOIN SC b on a.SID = b.SID LEFT JOIN Course c on b.CID = c.CID GROUP BY a.SID , a.Sname order BY ƽ���� desc

--18 ��ѯ���Ƴɼ���߷֡���ͷֺ�ƽ���֣�
--��������ʽ��ʾ���γ� ID���γ� name����߷֣���ͷ֣�ƽ���֣������ʣ��е��ʣ������ʣ�������
--����Ϊ>=60���е�Ϊ��70-80������Ϊ��80-90������Ϊ��>=90
--Ҫ������γ̺ź�ѡ����������ѯ����������������У���������ͬ�����γ̺���������
SELECT m.CID �γ̱�� , m.Cname �γ����� ,
(SELECT MAX(score) FROM SC WHERE CID = m.CID) ��߷� ,
(SELECT min(score) FROM SC WHERE CID = m.CID) ��ͷ� ,
(SELECT CAST(AVG(score) AS decimal(18,2)) FROM SC WHERE CID = m.CID) ƽ���� ,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 60)*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) ������,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 70 AND score < 80 )*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) �е��� ,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 80 AND score < 90 )*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) ������ ,
CAST((SELECT COUNT(1) FROM SC WHERE CID = m.CID AND score >= 90)*100.0 / (SELECT COUNT(1) FROM SC WHERE CID = m.CID) AS decimal(18,2)) ������
FROM Course m order BY m.CID

--19 �����Ƴɼ��������򣬲���ʾ������ Score �ظ�ʱ�������ο�ȱ
SELECT t.* , px = (SELECT COUNT(1) FROM SC WHERE CID = t.CID AND score > t.score) + 1 FROM sc t order BY t.cid , px

--20 �����Ƴɼ��������򣬲���ʾ������ Score �ظ�ʱ�ϲ�����
SELECT t.* , px = (SELECT COUNT(distinct score) FROM SC WHERE CID = t.CID AND score >= t.score) FROM sc t order BY t.cid , px

--21 ��ѯѧ�����ܳɼ����������������ܷ��ظ�ʱ�������ο�ȱ
SELECT t.* , px = rank() over(order BY �ܳɼ� desc) FROM (SELECT m.SID ѧ����� ,m.Sname ѧ������ ,isnull(SUM(score),0) �ܳɼ� FROM Student m LEFT JOIN SC n on m.SID = n.SID GROUP BY m.SID , m.Sname) t order BY px
--22 ��ѯѧ�����ܳɼ����������������ܷ��ظ�ʱ���������ο�ȱ
SELECT t.* , px = DENSE_RANK() over(order BY �ܳɼ� desc) FROM (SELECT m.SID ѧ����� ,m.Sname ѧ������ ,isnull(SUM(score),0) �ܳɼ� FROM Student m LEFT JOIN SC n on m.SID = n.SID GROUP BY m.SID , m.Sname) t order BY px

--23 ͳ�Ƹ��Ƴɼ����������������γ̱�ţ��γ����ƣ�[100-85]��[85-70]��[70-60]��[60-0] ����ռ�ٷֱ�
SELECT course.cname, course.cid,
SUM(cASe when sc.score<=100 AND sc.score>85 THEN 1 ELSE 0 end) AS "[100-85]",
SUM(cASe when sc.score<=85 AND sc.score>70 THEN 1 ELSE 0 end) AS "[85-70]",
SUM(cASe when sc.score<=70 AND sc.score>60 THEN 1 ELSE 0 end) AS "[70-60]",
SUM(cASe when sc.score<=60 AND sc.score>0 THEN 1 ELSE 0 end) AS "[60-0]"
FROM sc LEFT JOIN course
on sc.cid = course.cid
GROUP BY sc.cid,course.cname,course.cid;

--24 ��ѯ���Ƴɼ�ǰ�����ļ�¼
SELECT m.* , n.CID , n.score FROM Student m, SC n WHERE m.SID = n.SID AND n.score in (SELECT top 3 score FROM sc WHERE CID = n.CID order BY score desc) order BY n.CID , n.score desc

--25 ��ѯÿ�ſγ̱�ѡ�޵�ѧ����
SELECT Cid , COUNT(SID) ѧ���� FROM sc GROUP BY CID

--26 ��ѯ��ֻѡ�����ſγ̵�ѧ��ѧ�ź�����
SELECT Student.SID , Student.Sname FROM Student , SC WHERE Student.SID = SC.SID GROUP BY Student.SID , Student.Sname HAVING COUNT(SC.CID) = 2 order BY Student.SID

--27 ��ѯ������Ů������
SELECT COUNT(Ssex) AS �������� FROM Student WHERE Ssex = N'��'
SELECT COUNT(Ssex) AS Ů������ FROM Student WHERE Ssex = N'Ů'

--28 ��ѯ�����к��С��硹�ֵ�ѧ����Ϣ
SELECT * FROM student WHERE sname LIKE N'%��%'


--29 ��ѯͬ��ͬ��ѧ����������ͳ��ͬ������
SELECT Sname ѧ������ , COUNT(*) ���� FROM Student GROUP BY Sname HAVING COUNT(*) > 1

--30 ��ѯ 1990 �������ѧ������

SELECT * FROM Student WHERE CONVERT(VARCHAR(4),sage,120) = '1990'

--31 ��ѯÿ�ſγ̵�ƽ���ɼ��������ƽ���ɼ��������У�ƽ���ɼ���ͬʱ�����γ̱����������
SELECT m.CID , m.Cname , cast(avg(n.score) as decimal(18,2)) avg_score FROM Course m, SC n WHERE m.CID = n.CID GROUP BY m.CID , m.Cname ORDER BY avg_score DESC, m.CID asc

--32 ��ѯƽ���ɼ����ڵ��� 85 ������ѧ����ѧ�š�������ƽ���ɼ�
SELECT a.SID , a.Sname , cast(avg(b.score) as decimal(18,2)) avg_score FROM Student a , sc b WHERE a.SID = b.SID GROUP BY a.SID , a.Sname having cast(avg(b.score) as decimal(18,2)) >= 85 ORDER BY a.SID

--33 ��ѯ�γ�����Ϊ����ѧ�����ҷ������� 60 ��ѧ�������ͷ���
SELECT sname , score FROM Student , SC , Course WHERE SC.SID = Student.SID and SC.CID = Course.CID and Course.Cname = N'��ѧ' and score < 60

--34 ��ѯ����ѧ���Ŀγ̼��������������ѧ��û�ɼ���ûѡ�ε������
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID ORDER BY Student.SID , SC.CID

--35 ��ѯ�κ�һ�ſγ̳ɼ��� 70 �����ϵ��������γ����ƺͷ���
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score >= 70 ORDER BY Student.SID , SC.CID

--36 ��ѯ������Ŀγ�
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score < 60 ORDER BY Student.SID , SC.CID

--37 ��ѯ�γ̱��Ϊ 01 �ҿγ̳ɼ��� 80 �����ϵ�ѧ����ѧ�ź�����
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.CID = '01' and SC.score >= 80 ORDER BY Student.SID , SC.CID

--38 ��ÿ�ſγ̵�ѧ������
SELECT Course.CID , Course.Cname , count(*) ѧ������ FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname ORDER BY Course.CID , Course.Cname

--39 �ɼ����ظ�����ѯѡ�ޡ���������ʦ���ڿγ̵�ѧ���У��ɼ���ߵ�ѧ����Ϣ����ɼ�

SELECT top 1 Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course , Teacher WHERE Student.SID = SC.SID and SC.CID = Course.CID and Course.TID = Teacher.TID and Teacher.Tname = N'����' ORDER BY SC.score DESC

--40 �ɼ����ظ�������£���ѯѡ�ޡ���������ʦ���ڿγ̵�ѧ���У��ɼ���ߵ�ѧ����Ϣ����ɼ�
SELECT student.*, sc.score, sc.cid FROM student, teacher, course,sc 
WHERE teacher.tid = course.tid
AND sc.sid = student.sid
AND sc.cid = course.cid
AND teacher.tname = '����'
AND sc.score = (
    SELECT Max(sc.score) 
    FROM sc,student, teacher, course
    WHERE teacher.tid = course.tid
    AND sc.sid = student.sid
    AND sc.cid = course.cid
    AND teacher.tname = '����'
);

--41 ��ѯ��ͬ�γ̳ɼ���ͬ��ѧ����ѧ����š��γ̱�š�ѧ���ɼ�
SELECT m.* FROM SC m ,(SELECT CID , score FROM SC GROUP BY CID , score having count(1) > 1) n WHERE m.CID= n.CID and m.score = n.score ORDER BY m.CID , m.score , m.SID

--42 ��ѯÿ�Ź��ɼ���õ�ǰ����
SELECT t.* FROM sc t WHERE score in (SELECT top 2 score FROM sc WHERE CID = T.CID ORDER BY score DESC) ORDER BY t.CID , t.score DESC

--43 ͳ��ÿ�ſγ̵�ѧ��ѡ������������ 5 �˵Ŀγ̲�ͳ�ƣ���
SELECT Course.CID , Course.Cname , count(*) ѧ������ FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname having count(*) >= 5 ORDER BY ѧ������ DESC , Course.CID

--44 ��������ѡ�����ſγ̵�ѧ��ѧ��
SELECT student.SID , student.Sname FROM student , SC WHERE student.SID = SC.SID GROUP BY student.SID , student.Sname having count(1) >= 2 ORDER BY student.SID

--45 ��ѯѡ����ȫ���γ̵�ѧ����Ϣ
SELECT student.* FROM student WHERE SID in(SELECT SID FROM sc GROUP BY SID HAVING count(1) = (SELECT count(1) FROM course))

--46 ��ѯ��ѧ�������䣬ֻ���������
SELECT * , DATEDIFF(yy , sage , GETDATE()) ���� FROM student

--47 ���ճ����������㣬��ǰ���� < �������µ������������һ
SELECT * , case when right(convert(VARCHAR(10),GETDATE(),120),5) < right(convert(VARCHAR(10),sage,120),5) then DATEDIFF(yy , sage , GETDATE()) - 1 else DATEDIFF(yy , sage , GETDATE()) end ���� FROM student

--48 ��ѯ���ܹ����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0

--49 ��ѯ���ܹ����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1

--50 ��ѯ���¹����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0

--51 ��ѯ���¹����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1
