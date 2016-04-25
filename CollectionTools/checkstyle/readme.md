# only works if you have checkstyle-all-jar.jar in your CLASSPATH
# also, make sure to have metroplis_checkstyle_metrics.xml in the same folder or path correctly (this command assumes it's in the same folder as you execute

java com.puppycrawl.tools.checkstyle.Main -c metropolis_checkstyle_metrics.xml -f xml -o metropolis_import.xml src/folder

