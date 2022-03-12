<?php 
	include_once("db.php");

	if (isset($_POST["username"]) && !empty($_POST["username"]) && 
		isset($_POST["password"]) && !empty($_POST["password"])){

		AddUser($_POST["username"], $_POST["password"]);
	}
	else{
		echo "ERROR: Could not connect. ";
	}

	function AddUser($username, $password){
		GLOBAL $con;

		$sql = "INSERT INTO users (username, password) VALUES (?,?)";
		$st=$con->prepare($sql);
		if($st->execute(array($username, sha1($password)))){
			echo "User created successfully !";
			exit();
		}
		
	}
	

	//if username or password is null (not set)
	echo "SERVER: error, enter a valid username & password";
?>
