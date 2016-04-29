namespace Test.Metropolis.Parsers
{
    public class JavaMetricsHelper
    {
        public static int VG => 194;
        public static int DIT => 9;
        public static int MLOC => 266;
        public static int NOF => 3;

        private const string Xml =
            @"<?xml version='1.0' encoding='UTF-8'?>
<Metrics scope = 'hibernate-core' type='Project' date='2016-03-25' xmlns='http://metrics.sourceforge.net/2003/Metrics-First-Flat'>'

 <Metric id = 'VG' description ='McCabe Cyclomatic Complexity' max ='10' hint ='use Extract-method to split the method up'>
     <Values per = 'method' avg = '1.856' stddev = '3.651' max = '194' maxinrange='false'>
         <Value name = 'orderExprs' source ='SqlGeneratorBase.java' package ='org.hibernate.hql.internal.antlr' value ='{VG}' inrange='false'/>
     </Values>
 </Metric>
 <Metric id = 'DIT' description ='Depth of Inheritance Tree'>
     <Values per = 'type' avg = '1.413' stddev = '1.441' max = '9'>
         <Value name = 'SqlGeneratorBase' source ='SqlGeneratorBase.java' package ='org.hibernate.hql.internal.ast.tree' value ='{DIT}'/>
     </Values>
 </Metric>
 <Metric id = 'MLOC' description ='Method Lines of Code'>
      <Values per = 'method' total = '152134' avg = '5.804' stddev = '15.141' max = '613'>
         <Value name='orderExprs' source ='SqlGeneratorBase.java' package ='org.hibernate.hql.internal.antlr' value ='{MLOC}'/>
      </Values>
 </Metric> 
 <Metric id = 'NOF' description ='Number of Attributes'>
      <Values per = 'type' total = '7397' avg = '2.142' stddev = '7.138' max = '156'>
            <Value name='SqlGeneratorBase' source ='SqlGeneratorBase.java' package ='org.hibernate.hql.internal.antlr' value ='{NOF}'/>
      </Values> 
 </Metric>
</Metrics>";


        public static string GetXml()
        {
            return Xml.Replace("{VG}", VG.ToString())
                      .Replace("{DIT}", DIT.ToString())
                      .Replace("{MLOC}",MLOC.ToString())
                      .Replace("{NOF}", NOF.ToString());
        }
    }
}
