using System;
// On importe le bon namespace du fichier uGESRTP.cs
using uWatchtable; 

namespace FanucRoboguideTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipAddress = "127.0.0.1";

            uGESRTP srtpClient = new uGESRTP(ipAddress);

            srtpClient.PLC_PORT = 60008;

            try
            {
                Console.WriteLine($"Connexion à ROBOGUIDE ({ipAddress}:{srtpClient.PLC_PORT})...");
                
                
                srtpClient.initConnection();
                
                if (srtpClient.Connected)
                {
                    Console.WriteLine("Connecté avec succès !");

                    
                    ushort adresseRegistre = 3; 

                    
                    short valeurA_Ecrire = 54;
                

                    Console.WriteLine($"Écriture de la valeur {valeurA_Ecrire} dans R[{adresseRegistre}]...");
                    
                    srtpClient.write_R_WORD(adresseRegistre, valeurA_Ecrire);
                    srtpClient.write_R_DWORD(4, 8);
                    srtpClient.write_R_FLOAT(5, 9);
                    Console.WriteLine("Écriture réussie !");
                    

                    
                    Console.WriteLine("Lecture du registre...");
                    short donneesLues = srtpClient.read_R_WORD(adresseRegistre);
                    
                    if (donneesLues != null)
                    {
                        Console.WriteLine($"[SUCCÈS] Valeur lue dans le robot : {donneesLues}");
                    }
                    else
                    {
                        Console.WriteLine("[ERREUR] Impossible de lire la donnée.");
                    }
                }
                else
                {
                    Console.WriteLine("[ERREUR] Échec de la connexion réseau.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERREUR EXCEPTION] : {ex.Message}");
            }
            finally
            {
                if (srtpClient.Connected)
                {
                    srtpClient.Connected = false;
                    Console.WriteLine("Déconnecté.");
                }
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}