﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Zombie_Shootout_Game
{
    public partial class Level_3 : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerhealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 4;
        int score = 0;
        Random randNum = new Random();

        List<PictureBox> zombiesList = new List<PictureBox>();

        WindowsMediaPlayer player = new WindowsMediaPlayer();

        public Level_3()
        {
            InitializeComponent();
            RestartGame();
            player.URL = "C:\\Users\\AMStudent\\source\\repos\\Zombie Shootout Game\\Zombie Shootout Game\\Resources\\background theme4.m4a";
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            if (playerhealth > 1)
            {
                healthBar.Value = playerhealth;

            }
            else
            {
                gameOver = true;
                shooter.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }


            Ammo.Text = "Ammo: " + ammo;
            Kills.Text = "Kills: " + score;
            HighScore.Text = "Highscore: " + score * 2;


            if (goLeft == true && shooter.Left > 36)
            {
                shooter.Left -= speed;
            }
            if (goRight == true && shooter.Left + shooter.Width < this.ClientSize.Width)
            {
                shooter.Left += speed;
            }
            if (goUp == true && shooter.Top > 75)
            {
                shooter.Top -= speed;
            }
            if (goDown == true && shooter.Top + shooter.Height < this.ClientSize.Height)
            {
                shooter.Top += speed;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (shooter.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 10;
                    }
                }


                if (x is PictureBox && (string)x.Tag == "zombie")
                {
                    if (shooter.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerhealth -= 1;
                    }

                    if (x.Left > shooter.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    if (x.Left < shooter.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }

                    if (x.Top > shooter.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }

                    if (x.Top < shooter.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }


                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombiesList.Remove(((PictureBox)x));
                            MakeZombies();
                        }
                    }
                }
            }
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (gameOver == true)
            {
                GameTimer.Stop();
                this.Show();
                Close();
                GameOver_1 nextlevel = new GameOver_1();
                nextlevel.Show();
                GameTimer.Start();
            }


            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                shooter.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                shooter.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                shooter.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                shooter.Image = Properties.Resources.down;
            }


            if (score == 125)
            {
                GameTimer.Stop();
                this.Show();
                Close();
                Level_3_Complete nextlevel = new Level_3_Complete();
                nextlevel.Show();
                GameTimer.Start();
            }


            if (score == 125)
            {
                player.URL = "C:\\Users\\AMStudent\\source\\repos\\Zombie Shootout Game\\Zombie Shootout Game\\Resources\\background theme4.m4a";
                player.controls.play();
                player.controls.next();
                player.URL = "C:\\Users\\AMStudent\\source\\repos\\Zombie Shootout Game\\Zombie Shootout Game\\Resources\\background theme5.m4a";
            }


            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }

           
        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }



            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                Shootbullet(facing);
                player.URL = "C:\\Users\\AMStudent\\Desktop\\Top Down zombie shooting game\\Top Down zombie shooting game\\Resources\\gun_sound2.wav";
                player.URL = "C:\\Users\\AMStudent\\Desktop\\Top Down zombie shooting game\\Top Down zombie shooting game\\Resources\\gun_sound2.wav";



                if (ammo < 2)
                {
                    DropAmmo();
                }
            }

        }

        private void Shootbullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            Bullet shootBullet2 = new Bullet();
            Bullet shootBullet3 = new Bullet();

            shootBullet.direction = direction;
            shootBullet2.direction = direction;
            shootBullet3.direction = direction;

            shootBullet.bulletLeft = shooter.Left + (shooter.Width / 2);
            shootBullet.bulletTop = shooter.Top + (shooter.Height / 2);

            shootBullet2.bulletLeft = shooter.Left + (shooter.Width / 3);
            shootBullet2.bulletTop = shooter.Top + (shooter.Height / 3);

            shootBullet3.bulletLeft = shooter.Left + (shooter.Width / 4);
            shootBullet3.bulletTop = shooter.Top + (shooter.Height  / 4);

            shootBullet.MakeBullet(this);
            shootBullet2.MakeBullet(this);
            shootBullet3.MakeBullet(this);
        }


        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = randNum.Next(0, 900);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            shooter.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(12, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(12, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            shooter.BringToFront();

        }


        private void RestartGame()
        {


            shooter.Image = Properties.Resources.up;

            foreach (PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);
            }

            zombiesList.Clear();

            for (int i = 0; i < 5; i++)
            {
                MakeZombies();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerhealth = 100;
            score = 0;
            ammo = 10;




            GameTimer.Start();
        }
    }
    
}
