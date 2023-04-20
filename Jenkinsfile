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
		stage("Deliver") {
            steps {
                withCredentials([usernamePassword(credentialsId: 'dockerhub', passwordVariable: 'DH_PASSWORD', usernameVariable: 'DH_USERNAME')]) {
                    sh 'docker login -u $DH_USERNAME --password-stdin'
                    sh "docker compose push"
                }
            }
        }
	}
	post {
		always {
		  sh 'docker logout'
		}
	}
}