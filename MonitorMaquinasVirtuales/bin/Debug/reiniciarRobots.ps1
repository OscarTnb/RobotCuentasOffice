$VM = Get-VM | Where-Object {$_.State –eq 'Running'}
echo "Se Reiniciado las siguienes Maquinas: "
foreach ($actual in $VM){
	echo $actual.Name
    Restore-VMSnapshot -Name InicioRobot -VMName $actual.Name -Confirm:$False
    Start-Sleep -s 5
}
