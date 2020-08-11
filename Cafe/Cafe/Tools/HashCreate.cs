using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cafe.Tools
{
    public class HashCreate
    {
        const int saltSize = 16, hashSize = 20;
        byte[] hash, salt;

        string HasCreate(string password)
        {
            SaltCreate();
            hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 50,
                numBytesRequested: hashSize
                );
            return Convert.ToBase64String(hash);
        }

        byte[] HasCreate(string password, byte[] _salt)
        {

            hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 50,
                numBytesRequested: hashSize
                );
            return hash;
        }

        public string CreateHashString(string password)
        {
            HasCreate(password);
            var a = new byte[saltSize + hashSize];
            Array.Copy(salt, 0, a, 0, saltSize);
            Array.Copy(hash, 0, a, saltSize, hashSize);
            var mm=Convert.ToBase64String(a);
            return mm;

        }

        public bool Verify(string password, string HashWithSalt)
        {
            var b = Convert.FromBase64String(HashWithSalt);
            var saltOld = new byte[saltSize];
            Array.Copy(b, 0, saltOld, 0, saltSize);
            var newHash= HasCreate(password, saltOld);
            for (int i = 0; i < newHash.Length; i++)
                if (newHash[i] != b[i + saltSize]) return false;
            return true;
        }
        
        void SaltCreate()
        {
            salt = new byte[saltSize];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(salt);
            }
        }
    }
}
