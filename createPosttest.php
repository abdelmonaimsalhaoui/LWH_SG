<?php 
	include_once("db.php");

	if (isset($_POST["playerName"]) && !empty($_POST["playerName"]) && 
		isset($_POST["testName"]) && !empty($_POST["testName"]) &&
		isset($_POST["correctAnswers"]) && !empty($_POST["correctAnswers"]) &&
		isset($_POST["timeSpent"]) && !empty($_POST["timeSpent"])){

		AddPretest($_POST["playerName"], $_POST["testName"],$_POST["correctAnswers"],$_POST["timeSpent"]);
	}
	else{
		echo "ERROR: Ooops ! ";
	}

	function AddPretest($playerName, $testName, $correctAnswers, $timeSpent){
		GLOBAL $con;

		$sql = "INSERT INTO Posttests (playerName,testName,correctAnswers,timeSpent) VALUES (?,?,?,?)";
		$st=$con->prepare($sql);
		if($st->execute(array($playerName, $testName, $correctAnswers, $timeSpent))){
			echo "Posttest added successfully !";
			exit();
		}
		
	}
		
	echo "SERVER: error !";
?>
