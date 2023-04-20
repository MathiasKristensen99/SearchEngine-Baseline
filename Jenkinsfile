pipeline {
	agent any
	triggers {
		pollSCM("* * * * *")
	}
	stages {
		stage("Build") {
			sh "dotnet build"
			sh "docker compose build"
		}
	}
}