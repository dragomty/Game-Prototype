#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
#endregion

namespace GameStateManagementSample
{
    class WeaponsMenuScreen : MenuScreen
    {

        public WeaponsMenuScreen()
            : base("Weapons Menu")
        {
            TransitionOffTime = TimeSpan.FromSeconds(0.1);

            // Create our menu entries.
            MenuEntry RedBulletEntry = new MenuEntry("RedBullet");
            MenuEntry BlueBulletEntry = new MenuEntry("BlueBullet");
            MenuEntry BackEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            BackEntry.Selected += OnCancel;
            RedBulletEntry.Selected += RedBulletEntrySelected;
            BlueBulletEntry.Selected += BlueBulletEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(BackEntry);
            MenuEntries.Add(RedBulletEntry);
            MenuEntries.Add(BlueBulletEntry);
        }

        void BlueBulletEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            GameplayScreen.isBulletBlue = true; // the bullet is blue
            Console.WriteLine("bluebullet"); // confirm this

        }

        void RedBulletEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            GameplayScreen.isBulletBlue = false; // the bullet is red
            Console.WriteLine("redbullet"); // confirm this
        }

    }
}
