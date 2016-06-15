﻿/******************************************
* Projet : TalkEntreprise_client
* Description : création d'une messagerie instantanée
* Date : juin 2016
* Version : 1.0
* Auteur :Gabriel Strano
*
******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace talkEntreprise_client
{
    public class ManageMessages
    {
        //////Champs//////////////
        private Controler _ctrl;
        private DESCryptoServiceProvider _key;
        //////propriétées//////////////
        public DESCryptoServiceProvider Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        //////Constructeur//////////////
        public ManageMessages(Controler c)
        {
            this.Ctrl = c;
            this.Key = new DESCryptoServiceProvider();
            this.Key.Key = new byte[8] { 178, 107, 216, 40, 30, 50, 250, 253 };
            this.Key.IV = new byte[8] { 249, 169, 4, 183, 39, 35, 176, 26 };
        }
        //////méthodes//////////////
        /// <summary>
        /// permet de crypter le message en tableau de byte
        /// </summary>
        /// <param name="strText">message</param>
        /// <param name="key">clée d'encodage</param>
        /// <returns>messages crypté</returns>
        public static byte[] Encrypt(string strText, SymmetricAlgorithm key)
        {
            // Create a memory stream.
            MemoryStream ms = new MemoryStream();
            // Create a CryptoStream using the memory stream and the
            // CSP(cryptoserviceprovider) DES key.
            CryptoStream crypstream = new CryptoStream(ms, key.CreateEncryptor(key.Key, key.IV), CryptoStreamMode.Write);
            // Create a StreamWriter to write a string to the stream.
            StreamWriter sw = new StreamWriter(crypstream);
            // Write the strText to the stream.
            sw.WriteLine(strText);
            // Close the StreamWriter and CryptoStream.
            sw.Close();
            crypstream.Close();
            // Get an array of bytes that represents the memory stream.
            byte[] buffer = ms.ToArray();
            // Close the memory stream.
            ms.Close();
            // Return the encrypted byte array.
            return buffer;
        }
        /// <summary>
        /// permet de décripter le message
        /// </summary>
        /// <param name="encryptText">message encrypté</param>
        /// <param name="key">clée de cryptage</param>
        /// <returns>message décrypté</returns>
        public static string Decrypt(byte[] encryptText, SymmetricAlgorithm key)
        {
            // Create a memory stream to the passed buffer.
            MemoryStream ms = new MemoryStream(encryptText);
            // Create a CryptoStream using  memory stream and CSP DES key.
            CryptoStream crypstream = new CryptoStream(ms, key.CreateDecryptor(key.Key, key.IV), CryptoStreamMode.Read);

            // Create a StreamReader for reading the stream.
            StreamReader sr = new StreamReader(crypstream);

            // Read the stream as a string.
            string val = sr.ReadLine();

            // Close the streams.
            sr.Close();
            crypstream.Close();
            ms.Close();

            return val;
        }

/// <summary>
/// permet de décrypter un message
/// </summary>
/// <param name="message">message de l'utilisateur</param>
/// <returns>message codé</returns>
        public string EncryptMessage(string message)
        {
            string msg = string.Empty;
            byte[] EncryptedMessage = Encrypt(message, Key);

            for (int i = 0; i < EncryptedMessage.Length; i++)
            {
                if (i + 1 == EncryptedMessage.Length)
                {
                    msg += EncryptedMessage[i].ToString();
                }
                else
                {
                    msg += EncryptedMessage[i].ToString() + ",";
                }
            }
            return msg;
        }
        /// <summary>
        /// permet de décoder le message
        /// </summary>
        /// <param name="message">message codé</param>
        /// <returns>message original</returns>
        public string DecryptMessage(string message)
        {
            List<byte> EncryptedMessage = new List<byte>();
            foreach (string msgFrag in message.Split(','))
            {
                EncryptedMessage.Add(Convert.ToByte(msgFrag));
            }
            return Decrypt(EncryptedMessage.ToArray(), Key);

        }



    }
}
