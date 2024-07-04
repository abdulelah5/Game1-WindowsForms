using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed=14;        
        int force;
        int score = 0;
        int playerSpeed = 9;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 7;
        int enemyTwoSpeed = 5;
        int enemyThreeSpeed = 5;
        int enemyFourSpeed = 7;
        int enemyFiveSpeed = 7;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -7;
                force -= 1;
            }            

            else
            {
                jumpSpeed = 12;                
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false ||
                                (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;

                            }
                        }

                        x.BringToFront();
                    }

                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "score: " + score + Environment.NewLine
                                + "Game over Loser whew";
                        }
                    }

                }
            }

            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 ||
                horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top -= verticalSpeed;
            if (verticalPlatform.Top < 140 || verticalPlatform.Top > 410)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;
            if(enemyOne.Left < pictureBox2.Left || 
                enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left -= enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox4.Left ||
                enemyTwo.Left + enemyTwo.Width > pictureBox4.Left + pictureBox4.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            enemyThree.Left -= enemyThreeSpeed;
            if (enemyThree.Left < pictureBox5.Left ||
                enemyThree.Left + enemyThree.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyThreeSpeed = -enemyThreeSpeed;
            }

            enemyFour.Left -= enemyFourSpeed;
            if (enemyFour.Left < pictureBox6.Left ||
                enemyFour.Left + enemyFour.Width > pictureBox6.Left + pictureBox6.Width)
            {
                enemyFourSpeed = -enemyFourSpeed;
            }

            enemyFive.Left -= enemyFiveSpeed;
            if (enemyFive.Left < pictureBox8.Left ||
                enemyFive.Left + enemyFive.Width > pictureBox8.Left + pictureBox8.Width)
            {
                enemyFiveSpeed = -enemyFiveSpeed;
            }


            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "score: " + score + Environment.NewLine +
                    "You fell to your death loser";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 13)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "score: " + score + Environment.NewLine +
                    "It is just lucky loser";
            }
            if (player.Bounds.IntersectsWith(door.Bounds) && score < 13)
            {
                txtScore.Text = "score: " + score + Environment.NewLine +
                    "No no no Collect all the coins first";
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Up && jumping == false)
            {
                jumping = true;
            }

            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }            

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (jumping == true)
            {
                jumping = false;
            }            

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                restartGame();
            }


        }

        private void restartGame()
        {
            jumping = false;            
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score: " + score;

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reset the position of the player
            player.Left = 123;
            player.Top = 355;

            enemyOne.Left = 374;        
            enemyTwo.Left = 225;            
            enemyThree.Left = 425;            
            enemyFour.Left = 323;            
            enemyFive.Left = 150;            

            horizontalPlatform.Left = 185;
            verticalPlatform.Top = 315;

            gameTimer.Start();


        }
    }
}
