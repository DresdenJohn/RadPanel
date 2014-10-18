 <?php
	class MSSQL
	{
		public $query = null;
		public $fetch = null;
		protected $fetca;
		protected $mfetc;
    
		public function __construct ($fetch = false, $multi = false)
		{
			$fetch ? $this->fetca = true : $this->fetca = false;
			$multi ? $this->mfetc = true : $this->mfetc = false;
		}
		
		private function Escape($arg)
		{
			foreach ($arg as $value)
				/*substr($value, 0, 1) != '0' &&*/ is_numeric($value) ? $return[] = "'".$value."'" : $return[] = "0x".bin2hex($value);
			return $return;
		}

		public function Exec()
		{
			if( !func_num_args() )
				return false;
				
			$arg = func_get_args();
			
			$query = $arg[0];
			unset($arg[0]);
			
			if( count( $arg ) )
				$query = vsprintf( $query, $this->Escape( $arg ) );
		
			$mssql = odbc_connect('Driver={SQL Server Native Client '.MSSQL_VERS.'.0};Server='.MSSQL_HOST.';Port='.MSSQL_PORT.';Database=master;Trusted_Connection='.( MSSQL_TRUS ? 'yes' : 'no' ).';', MSSQL_USER, MSSQL_PASS);
			$this->query = odbc_exec( $mssql, $query );
			
			if( $this->fetca )
			{
				if( $this->mfetc )
					while( $fetch = odbc_fetch_array( $this->query ) )
						$this->fetch[] = $fetch;
				else
					$this->fetch[] = odbc_fetch_array( $this->query );
			}
			
			if( is_resource( $this->query ) )
				odbc_free_result( $this->query );
				
			odbc_close( $mssql );
			return true;
		}
		
		public function m_fetch()
		{
			$return = null;			
			$i = 0;			
			while( isset( $this->fetch[$i] ) )
			{
				$return[] = $this->fetch[$i];
				$i++;
			}
			return $return;
		}
	}
?> 