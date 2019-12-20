CP := bin
CP := $(CP):$(HOME)/.m2/repository/junit/junit/4.11/junit-4.11.jar
CP := $(CP):$(HOME)/.m2/repository/org/hamcrest/hamcrest-core/1.3/hamcrest-core-1.3.jar

clean:
	rm -rf bin

prepare:
	[[ -d bin ]] || mkdir bin;

compile_prod: clean prepare
	javac -d bin -cp $(CP) src/main/java/org/jtodd/aoc/asteroids/*java

compile_test: compile_prod
	javac -d bin -cp $(CP) src/test/java/org/jtodd/aoc/asteroids/*java

run_test: compile_test
	cp src/test/resources/*txt bin
	java -cp $(CP) org.junit.runner.JUnitCore org.jtodd.aoc.asteroids.TestMonitoringStation
