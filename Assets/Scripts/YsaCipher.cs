// Fonctionellement, le cipher effectue la suite d'opérations suivantes dans l'ordre :
// 1. Convertit le score en Base64, et retire le token == à la fin
// 2. Allonge le résultat obtenu artificiellement de manière à obtenir une clé de 15 charactères
// 3. Effectue un vigenère alphanumérique de clé de ton choix dessus.
// 4. Effectue un shuffle sur la clé chiffrée.

// L'opératin 2 implique la création de FAUSSES DONNÉES, ce qui veut dire que retourner dans l'autre sens va générer 15 scores potentiels différents.
// Il suffira de vérifier que le score original est bien dedans.

using System;
using System.Text;
using static System.Random;

public class YsaCipher
{
    private static char[] alphabet;
    private static char[] numbers;
    private static string alphanum;

   // public String final;

    public static string Main(string[] args)
    {
        alphanum = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        Console.WriteLine($"");
        string score = args[0];
        string key = args[1];
        try
        {
            int.Parse(args[0]);
            Console.WriteLine($"Score entré : {score}");
        }
        catch (System.FormatException)
        {
            Console.WriteLine(
                $"Erreur. L'argument 1 : \"{args[0]}\" n'a pas pu être converti en entier."
            );
        }

        for (int i = 0; i < key.Length; i++)
        {
            if (!(alphanum.Contains(key[i])))
            {
                Console.WriteLine(
                    $"Erreur. Clé invalide : \"{key[i]}\" n'est pas un charactère alphanumérique."
                );
                System.Environment.Exit(0);
            }
        }

        Console.WriteLine($"Clef entrée : {key}");
        Console.WriteLine("");

        String final = MoutonCipher(score,key);
        return final;
        
    }

    private static string MoutonCipher(string enter, string key, bool invert = false) {
        // PARTIE 1
        // CONVERTION EN BASE64

        string score64 = ToBase64(enter);
        Console.WriteLine($"PARTIE I -- 1. Passage en Base64. Score64 (avec padding) = {score64}");

        while (score64[score64.Length - 1] == '=')
        {
            score64 = score64.Remove(score64.Length - 1, 1);
        }

        Console.WriteLine(
            $"PARTIE I -- 2. Elimination du padding. Score64 (sans padding) = {score64}"
        );

        // PARTIE 2
        // ON ALLONGE JUSQUE 15 CHARACTÈRES

        int nb_missing_chars = 15 - score64.Length;
        Console.WriteLine(
            $"PARTIE II -- 1. Calcul du nombre de chars manquants. Longueur de score64 = {score64.Length}. Chars manquants : {nb_missing_chars}"
        );

        string random_padding = "";
        Random rng = new Random();
        for (int i = 0; i < nb_missing_chars; i++)
        {
            random_padding = random_padding + alphanum[rng.Next(0, alphanum.Length)];
        }

        score64 += random_padding;
        Console.WriteLine(
            $"PARTIE II -- 2. Création d'un padding aléatoire. Padding calculé = {random_padding}. Score64Padded : {score64}"
        );

        // PARTIE 3
        // VIGENERE

        char[] score_crypted_array = score64.ToCharArray();
        for (int i = 0; i < score_crypted_array.Length; i++)
        {
            score_crypted_array[i] = alphanum[
                (
                    Array.IndexOf(alphanum.ToCharArray(), score_crypted_array[i])
                    + Array.IndexOf(alphanum.ToCharArray(), key[i % key.Length]) 
                ) % alphanum.Length
            ];
        }
        string score_crypted = new string(score_crypted_array);
        Console.WriteLine(
            $"PARTIE III -- 1. Cryptage vigenère. Clé : {key}. ScoreCrypté : {score_crypted}"
        );

        // PARTIE 4
        // LE MOUTONSHUFFLE
        string final_ID = MoutonShuffle(score_crypted);
        Console.WriteLine(
            $"PARTIE IV -- 1. Application du MoutonShuffle. Identifiant final : {final_ID}"
        );

        return final_ID;
    }

    private static string ToBase64(string arg, bool invert = false)
    {
        if (invert)
        {
            byte[] decodedBytes = Convert.FromBase64String(arg);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            return decodedText;
        }
        else
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(arg);
            string encodedText = Convert.ToBase64String(bytesToEncode);
            return encodedText;
        }
    }

    private static string MoutonShuffle(string str_in, bool invert = false) {
        char[] str_out = "aaaaaaaaaaaaaaa".ToCharArray() ;
        str_out[0] = str_in[7];
        str_out[1] = str_in[6];
        str_out[2] = str_in[8];
        str_out[3] = str_in[5];
        str_out[4] = str_in[9];
        str_out[5] = str_in[4];
        str_out[6] = str_in[10];
        str_out[7] = str_in[3];
        str_out[8] = str_in[11];
        str_out[9] = str_in[2];
        str_out[10] = str_in[12];
        str_out[11] = str_in[1];
        str_out[12] = str_in[13];
        str_out[13] = str_in[0];
        str_out[14] = str_in[14];
        return new string (str_out);
    }
}
