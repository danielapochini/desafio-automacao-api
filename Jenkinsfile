pipeline {
    agent any
    environment {
        nuget = "C:\\jenkins\\nuget.exe"  
    }
    stages {
        stage('Git Checkout') { 
           steps{ 
                bat 'echo Git Checkout'
                git branch: 'main', credentialsId: '', url: 'https://github.com/danielapochini/desafio-automacao-api.git'
                //checkout([$class: 'GitSCM', branches: [[name: '*/main']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: '41cd41b9-18c3-49fa-bd4b-24df52b50cc8', url: 'https://github.com/danielapochini/desafio-automacao-api.git']]])
            }
        }
        stage('CleanUp Stage') { 
            steps{ 
                bat 'echo CleanUp Stage'
                dotnetClean configuration: 'DEV', project: 'DesafioAutomacaoAPI.sln', sdk: '.NET 5.0', workDirectory: "${WORKSPACE}/DesafioAutomacaoAPI/"
            }
        }
        stage('Restore Package Stage') { 
           steps{ 
                bat 'echo Restore Package Stage'
                dotnetRestore project: 'DesafioAutomacaoAPI.sln', sdk: '.NET 5.0', workDirectory: "${WORKSPACE}/DesafioAutomacaoAPI/"
            } 
        }
        stage('Build Stage') { 
            steps{ 
                bat 'echo build'
                dotnetBuild configuration: 'DEV', project: 'DesafioAutomacaoAPI.sln', sdk: '.NET 5.0', workDirectory: "${WORKSPACE}/DesafioAutomacaoAPI/"
            }
        }
        stage('Test Execution Stage') { 
            steps{ 
                bat 'echo Test Execution Started'
               dotnetTest configuration: 'DEV', project: 'DesafioAutomacaoAPI.sln', sdk: '.NET 5.0', workDirectory: "${WORKSPACE}/DesafioAutomacaoAPI/"
            }
        } 
        
        stage('Report'){
            steps {
                fileOperations([fileCopyOperation(excludes: '', flattenFiles: true, includes: 'DesafioAutomacaoAPI/Utils/Resources/Allure/*.properties', renameFiles: false, sourceCaptureExpression: '', targetLocation: 'DesafioAutomacaoAPI/bin/DEV/net5.0/allure-results', targetNameExpression: '')])
                allure includeProperties: false, jdk: '', results: [[path: 'DesafioAutomacaoAPI/bin/DEV/net5.0/allure-results']]
            } 
        }
    } 
}
