$baseUrl = "https://somesite.azurewebsites.net/api/sitecore/LongRunning"
$resp = Invoke-WebRequest -Uri "$baseUrl/Start?delay=300000" -Method Post -UseBasicParsing

$jobName = ($resp.Content | ConvertFrom-Json).jobName
Write-Host "jobName is $jobName"

do
{
    $resp1 = Invoke-WebRequest -Uri "$baseUrl/JobStatus?jobName=$jobName" -Method Post -UseBasicParsing
    $status = ($resp1.Content | ConvertFrom-Json).status
    Write-Host "status is $status"
    Start-Sleep -Seconds 2
}
while($status -eq "Running")

$resp1 = Invoke-WebRequest -Uri "$baseUrl/JobStatus?jobName=$jobName" -Method Post -UseBasicParsing
$status = ($resp1.Content | ConvertFrom-Json).status
Show-Confirm -Title "Job $jobName is $status"