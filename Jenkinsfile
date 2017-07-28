properties([
  parameters([string(description: 'Repository Account', name: 'REPO_ACCT')])
])


podTemplate(
	name: 'dotnet-and-docker',
	label: 'dotnet-and-docker',
	containers: [
		containerTemplate(name: 'dotnet-core',image: 'microsoft/dotnet:1.1.2-sdk',ttyEnabled: true,command: 'cat'), 
		containerTemplate(name: 'awscli', image: 'teardrop/awscli', ttyEnabled: true, command: 'cat'),
		containerTemplate(name: 'docker', image: 'docker:stable-dind', ttyEnabled: true, command: 'cat', privileged: true),
	],
	annotations: [
		podAnnotation(key: "kube2iam.beta.nordstrom.net/role", value: "${env.K2IAM_ROLE_V2_NONPROD}")
	], 
	volumes: [
		emptyDirVolume(mountPath: '/var/lib/docker', memory: false),
		hostPathVolume(hostPath: '/var/run/docker.sock', mountPath: '/var/run/docker.sock')
	]		
)
{
	node('dotnet-and-docker') {

		checkout scm

		stage ('Test Influxdb Job') 
		{
			container('dotnet-core') {
			     sh 'dotnet restore && dotnet test InfluxDBJob/InfluxDBTests.csproj --filter Category!=Integration'
			}		
		}

		stage ('AWS Install')
		{
			container('awscli')
			{
				sh '''
				aws ecr get-login --no-include-email --region us-west-2 > ecr-login
				'''
			}
		}

		stage ('Docker build and push image')
		{
			container('docker')
			{
					sh '''
							set +x
							eval $(cat ecr-login)
					'''
					sh """
						docker build -f Dockerfile -t influxdb-backend-job:latest .
						docker tag influxdb-backend-job:latest ${params.REPO_ACCT}.dkr.ecr.us-west-2.amazonaws.com/influxdb-backend-job:latest
						docker push ${params.REPO_ACCT}.dkr.ecr.us-west-2.amazonaws.com/influxdb-backend-job:latest
					"""
			}
		}
	}
}




