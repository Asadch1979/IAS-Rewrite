       public UserModel AutheticateLogin(LoginModel login)
           {
                isAuthenticate = false,
                DatabaseDown = false
            try
                {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                }
            catch (Exception)
                {
                user.DatabaseDown = true;
                user.ErrorMsg = "System under maintenance";
                }
            finally
                {
                if (con != null)
                    con.Dispose();
                }
