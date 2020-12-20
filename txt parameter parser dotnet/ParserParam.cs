using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

//такие тестовые документы "DocForPars.txt" с кучей параметров мне нужны были для 
//решения задач вывода математических параметров, посчитанных в мат.лабе к примеру.

public class ParameterParser {
	
	public string nameFile { private get; set; }
	
	public List<List<float>> parsingParametersInFloat = new List<List<float>>();
	
	public List<String> newParametrsStringTipe;
	
	public string[] buckUpParam;
	
	public void GetParametersFromTxtFile() {
			StreamReader sr = new StreamReader(nameFile);
			BuckUpParam = new string[newParametrsStringTipe.Count];
			for (int i = 0; i < newParametrsStringTipe.Count; i++) {
				BuckUpParam[i] = newParametrsStringTipe[i];
			}
			newParametrsStringTipe.Clear();
			List<string> line = new List<string>();
			for (int i=0; !sr.EndOfStream; i++) {
				newParametrsStringTipe.Add("");
				newParametrsStringTipe[i] = sr.ReadLine();
			}
            } catch {
			MessageBox.Show("txt file not exist");
		}
	}
	
	public void ConvertParamToFloat() {
		string [] param;
		float number;
		for(int i=0; i< newParametrsStringTipe.Count; i++) {
			parsingParametersInFloat.Add(new List<float>());
			param = newParametrsStringTipe[i].Split(';');
			for (int j=0; j<param.Length;j++){   
				if (Single.TryParse(param[j], out number)) {
					parsingParametersInFloat[i].Add(number);
				} 
			}
			Array.Clear(param, 0, param.Length);
		}
	}
	
	public void ClearParam() {
		newParametrsStringTipe.Clear();
	}
	
}