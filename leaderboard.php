<?php 
	include_once("db.php");

	getLeaderboard();

	function getLeaderboard(){
		GLOBAL $con;

		$sql = "SELECT playerName,playerPoints from leaderboard";
		$st=$con->prepare($sql);
		if($st->execute()){
			$all=$st->fetchAll(PDO::FETCH_ASSOC);
			echo json_encode($all);
		}
	}

?>
