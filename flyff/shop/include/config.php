<?php
	session_start();
	
	// Connection Variable
	define( "MSSQL_HOST", "." );		// Host
	define( "MSSQL_USER", "dresdendoge" );							// User
	define( "MSSQL_PASS", "agentABC123" );							// Pass
	define( "MSSQL_PORT", "1433" );						// Port (default: 1433)
	define( "MSSQL_VERS", "10" );						// Driver version ( 10 => SQL2008, 11 => SQL2012 )
	define( "MSSQL_TRUS", true );						// Trusting connection ( Localhost => true, Remote => false )
	
	// Class includes
	include( "class/class.odbc.php" );
	
	// CDPSrvr
	define( "BILLING_IP",		"127.0.0.1" );			// Billing IP
	define( "BILLING_PORT",		29000 );				// Billing Port
	define( "BILLING_CHECK1", 	0000000 );				// Billing first check value
	define( "BILLING_CHECK2", 	0000000 );				// Billing second check value
	
	// Site config
	$language = 'EN';									// Shop language ( DE => German, EN => English )
	$site_title = 'Premium Shop';						// Shop title
	$charge_url = 'http://localhost/shop/';				// Charge url
	$cash_name = 'RadPoints';		 					// Currency name
	$cash_short = 'RP';									// Currency name (short)
	$hash_password = 'flight';							// Password hash
	$hash_shop = 'gaajgussfdbfhjq';						// Shop hash
	
	// Language include
	include( 'language.php' );
	
	$shop_cat = array( 'A' => $lang['cat_a'], 'B' => $lang['cat_b'], 'C' => $lang['cat_c'], 'D' => $lang['cat_d'], 'E' => $lang['cat_e'],
					   'F' => $lang['cat_f'], 'G' => $lang['cat_g'], 'H' => $lang['cat_h'], 'I' => $lang['cat_i'], 'J' => $lang['cat_j'] );
	
	// Login handling
	include( 'login.php' );
?>