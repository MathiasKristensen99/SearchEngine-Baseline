pipeline {
	agent any
	triggers {
		pollSCM("* * * * *")
	}
	stages {
		stage("Build") {
			steps{
				sh "dotnet build"
				sh "docker compose build"
			}
		}
	}
}