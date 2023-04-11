using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SaveLoadPlayerPrefs
{
    public enum SaveStrings
    {
        MONETARY_VALUE,
        HIGHSCORE,
        BLOOM,
        FILMGRAIN,
        CHROMATIC_ABERRATION,
        VOLUME,
        RESOLUTION,
        FULLSCREEN,
        SFX,
        FIRSTUSE
    }
    public class SaveLoad
    {
        public void SavingCoins(int coinsToSave)
        {
            if (PlayerPrefs.HasKey("MONETARY_VALUE")) PlayerSaveInt("MONETARY_VALUE", coinsToSave);
        }

        public int LoadingCoins()
        {
            return PlayerPrefs.GetInt("MONETARY_VALUE");
        }

        public void SpendingCoins(int spendCoins)
        {
            if (PlayerPrefs.HasKey("MONETARY_VALUE"))
            {
                int a = 0;
                a = PlayerPrefs.GetInt("MONETARY_VALUE");

                int total = a - spendCoins;

               PlayerSaveInt("MONETARY_VALUE", total);
            }
        }

        public void SaveScore(int scoreToSave)
        {
            PlayerSaveInt("HIGHSCORE", scoreToSave);
        }

        public int LoadingScore()
        {
            return PlayerPrefs.GetInt("HIGHSCORE");
        }

        public bool LoadingBool(string str)
        {
            return PlayerPrefs.GetString(str) == "True" ;
        }
        public void PlayerSaveInt(string str, int value)
        {
            PlayerPrefs.SetInt(str, value);
            PlayerPrefs.Save();
        }
        public void PlayerSaveFloat(string str, float value)
        {
                PlayerPrefs.SetFloat(str, value);
                PlayerPrefs.Save();
        }

        public void PlayerSaveBool(string str, bool value)
        {
            PlayerPrefs.SetString(str, value.ToString());
            PlayerPrefs.Save();
        }
    }
}
