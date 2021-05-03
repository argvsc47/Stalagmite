using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Stalagmite {

	class StalagmiteCLI {

		//Builtins
		static void netr(string[] args) {
			if (args[1] == "get" & args.Length != 3) {
				Console.WriteLine("Invalid Syntax.");
				return;
			} else if (args[1] == "post" & args.Length != 4) {
				Console.WriteLine("Invalid Syntax.");
				return;
			} else if (!(args[1] == "get" | args[1] == "post")) {
				Console.WriteLine(args[1]);
				Console.WriteLine("Unknown / Unsupported method.");
				return;
			}

			try {
				WebRequest request = WebRequest.Create(args[2]);

				if (args[1] == "get") {
					request.Method = "GET";

					WebResponse response = request.GetResponse();
					Console.WriteLine("Sending Get request to url: {0}..", args[2]);
					Console.WriteLine("Fetching response from url: {0}..", args[2]);
					Stream stream = response.GetResponseStream();

					StreamReader reader = new StreamReader(stream);
					string data = reader.ReadToEnd();
					Console.WriteLine("Successfully Fetched data from url: {0}.", args[2]);

					Console.WriteLine(data);
				} else if (args[1] == "post") {
					request.Method = "POST";
					request.ContentType = "application/json";

					StreamWriter writer = new StreamWriter(request.GetRequestStream());
					writer.Write(File.ReadAllText(args[3]));
					Console.WriteLine("Sending Post request to url: {0} with json: {1}..", args[2], args[3]);
					Console.WriteLine("Fetching response from url: {0}..", args[2]);
					
					WebResponse response = request.GetResponse();
					Stream stream = response.GetResponseStream();

					StreamReader reader = new StreamReader(stream);
					string data = reader.ReadToEnd();
					Console.WriteLine("Successfully Fetched data from url: {0}.", args[2]);

					Console.WriteLine(data);
				}
			} catch {
				Console.WriteLine("Error executing command.");
			}
		}

		static void dlf(string[] args) {
			if (args.Length != 2) {
				Console.WriteLine("Invalid Syntax.");
				return;
			}

			if (args[1] == "*") {
				Console.WriteLine("Deleting All Files..");
				DirectoryInfo cdir = new DirectoryInfo(Directory.GetCurrentDirectory());

				foreach (FileInfo file in cdir.GetFiles()) {
					Console.WriteLine("Deleting File {0}..", args[1]);
					File.Delete(file.Name);
					Console.WriteLine("Successfully deleted File {0}.", args[1]);
				}
			} else {
				if (File.Exists(args[1]) == false) {
					Console.WriteLine("Inexistant File.");
				} else {
					Console.WriteLine("Deleting File {0}..", args[1s])
					File.Delete(args[1]);
					Console.WriteLine("Successfully deleted File {0}.", args[1])
				}
			}
		}

		static void wipe(string[] args) {
			if (args.Length != 2) {
				Console.WriteLine("Invalid Syntax");
				return;
			}

			if (Directory.Exists(args[1]) == false) {
				Console.WriteLine("Inexistant Directory.")
			}
			Console.WriteLine("Wiping Directory {0}..", args[1])
			Directory.Delete(args[1], true);
			Console.WriteLine("Successfully wiped Directory {0}.", args[1])
		}

		static void ls(string[] args) {

			DirectoryInfo cdir;

			if (args.Length == 1) {
				cdir = new DirectoryInfo(Directory.GetCurrentDirectory());
			} else if (args.Length == 2) {
				cdir = new DirectoryInfo(args[1]);
			} else {
				Console.WriteLine("Invalid Syntax.");
				return;
			}

			foreach (DirectoryInfo dir in cdir.GetDirectories()) {
				Console.WriteLine("{0, -25}\t DIR", dir.Name);
			}

			foreach (FileInfo file in cdir.GetFiles()) {
				Console.WriteLine("{0, -25}\t FILE", file.Name);
			}
		}

		static void cd(string[] args) {
			if (args.Length != 2) {
				Console.WriteLine("Invalid Syntax.");
				return;
			}
			Directory.SetCurrentDirectory(args[1]);
			Console.WriteLine("Changed Current Directory to {0}.", args[1])
		}

		static void var(string[] args, Dictionary<string,string> shev) {
			if (args.Length != 3) {
				Console.WriteLine("Invalid Syntax.");
				return;
			}

			shev[args[1]] = args[2];
		}

		static void exec(string[] args) {
			if (args.Length < 2) {
				Console.WriteLine("Invalid Syntax.");
				return;
			}

			Console.WriteLine("Starting Program {0}..", args[1]);
			Process process = new Process();
			process.StartInfo.FileName = args[1];
			process.StartInfo.Arguments = String.Join(' ',args[2..args.Length]);

			process.Start();
			Console.WriteLine("Successfully started Program {0}.", args[1])
			process.WaitForExit();
		}

		static void nwd(string[] args) {
			if (args.Length != 2) {
				Console.WriteLine("Invalid Syntax.");
				return;
			}

			if (Directory.Exists(args[1])) {
				Console.WriteLine("{0} Directory already Exists.", args[1]);
			} else {
				Directory.CreateDirectory(args[1]);
				Console.WriteLine("Successfully created Directory {0}", args[1])
			}
		}

		static void rmd(string[] args) {
			if (args.Length != 2) {
				Console.WriteLine("Invalid Syntax.");
				return;
			}

			if (args[1] == "*") {
				Console.WriteLine("Deleting All Directories.");
				DirectoryInfo cdir = new DirectoryInfo(Directory.GetCurrentDirectory());

				foreach (DirectoryInfo dir in cdir.GetDirectories()) {
					Console.WriteLine("Deleting Directory {0}..", dir.Name);
					Directory.Delete(dir.Name);
					Console.WriteLine("Successfully deleted Directory {0}.");
				}
				Console.WriteLine("Successfully deleted All Directories.");
			} else {
				if (Directory.Exists(args[1]) == false) {
					Console.WriteLine("Inexistant Directory {0}.", args[1]);
				} else {
					Console.WriteLine("Deleting Directory, {0}...", args[1]);
					Directory.Delete(args[1]);
					Console.WriteLine("Successfully deleted Directory {0}.", args[1])
				}
			}
		}

		//

		static string[] parse_cmd(string input) {
			return input.Split(' ');
		}

		static int execute(string line, Dictionary<string,string> shev) {
			string[] cmd = parse_cmd(line);

			switch (cmd[0]) {
				case "netr":
					netr(cmd);
					break;
				case "dlf":
					dlf(cmd);
					break;
				case "wipe":
					wipe(cmd);
					break;
				case "ls":
					ls(cmd);
					break;
				case "cd":
					cd(cmd);
					break;
				case "var":
					var(cmd, shev);
					break;
				case "exec":
					exec(cmd);
					break;
				case "nwd":
					nwd(cmd);
					break;
				case "rmd":
					rmd(cmd);
					break;
				case "exit":
					return 0;
				default:
					Console.WriteLine("Unknown Command {0}", cmd[0]);
					break;
			}

			return 1;
		}

		static void Main(string[] args) {
			/*
			load env/config
			loop
			exit
			*/

			//loadConfig
			Dictionary<string,string> env = new Dictionary<string,string>();

			//loop

			while (true) {
				Console.Write("$> ");

				if (execute(Console.ReadLine(), env) == 0) {
					break; //exit
				}
			}
		}
	}
}