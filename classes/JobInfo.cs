using JobHunt_API.Record;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using JobHunt_API.models;

namespace JobHunt_API
{
    public class JobInfo
    {

        
        ///Connection String
        string cs = @"
            server=thor.mysql;
            userid=jobhuntapi;
            password=12191990;
            database=jobhunt
        ";
        MySqlConnection con;
        /// <summary>
        /// Returns data from jobhunt database.
        /// Opens and closes database connection with constructor and
        /// destructor.
        /// </summary>
        public JobInfo()
        {
            this.con = new MySqlConnection(cs);
            this.con.Open();
        }
        private void ExecuteSQL(string sql)
        {
            using MySqlCommand cmd = new MySqlCommand(sql, this.con);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            this.con.Close();
        }

        /// <summary>
        /// Returns Jobs after 'begindate' and before, and including the 'enddate'.
        /// </summary>
        /// <param name="begindate">jobs after this date will be included</param>
        /// <param name="enddate">jobs before, and including this date will be included </param>
        /// <returns></returns>
        public GetJobInfo[] ReturnJobs(string begindate, string enddate)
        {   
            
            return null;
        }

        public void UpdateJobDescription(string ID, string description)
        {
            string sql = @$"UPDATE `jobhunt`.`jobs` SET `JobDescription` = '{description}'
                        WHERE (`ID` = '{ID}')";
            
            this.ExecuteSQL(sql);
        }

        public void UpdateLocation(string ID, string city, string state)
        {
            string sql = @$"UPDATE `jobhunt`.`jobs` SET `State` = '{state}', `City` = '{city}'
                        WHERE (`ID` = '{ID}')";
            
            this.ExecuteSQL(sql);
        }

        public void UpdateRemoteHybridOnsite(string ID, bool remote, bool hybrid, bool onsite)
        {
            string sql = @$"UPDATE `jobhunt`.`jobs`
                            SET 
                            `Remote` = '{remote}',
                            `Hybrid` = '{hybrid}',
                            `Onsite` = '{onsite}'
                            WHERE (`ID` = '{ID}')";

            this.ExecuteSQL(sql);
        }

        public void UpdateResponded(string ID, bool responded)
        {
            string sql = @$"UPDATE `jobhunt`.`jobs` SET `Responded` = '{responded}'
                        WHERE (`ID` = '{ID}')";

            this.ExecuteSQL(sql);
        }

        public void UpdateSiteFoundOn(string ID, string SiteFoundOn)
        {
            string sql = @$"UPDATE `jobhunt`.`jobs` SET `SiteFoundOn` = '{SiteFoundOn}'
                    WHERE (`ID` = '{ID}')";

            this.ExecuteSQL(sql);
        }

        ~JobInfo()
        {
            this.con.Close();
        }

    }
}

