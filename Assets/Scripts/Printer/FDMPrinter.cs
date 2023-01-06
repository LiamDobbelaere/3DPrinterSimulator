using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class FDMPrinter : MonoBehaviour {
    public TextAsset gcode;
    public MoveOnAxis xAxis;
    public MoveOnAxis yAxis;
    public MoveOnAxis zAxis;
    public TrailRenderer trail;

    private GcodeParser gcodeParser;
    private int gcodePosition;
    private int lastGcodePosition;
    private string[] gcodeLines;

    // Start is called before the first frame update
    void Start() {
        gcodeParser = new GcodeParser();

        gcodeLines = gcode.text.Split('\n');
        lastGcodePosition = -1;
    }

    // Update is called once per frame
    void Update() {
        if (!xAxis.IsMoving() && !yAxis.IsMoving() && !zAxis.IsMoving() && gcodePosition < gcodeLines.Length) {
            gcodePosition++;
        }

        if (lastGcodePosition != gcodePosition) {
            lastGcodePosition = gcodePosition;

            string currentGcodeLine = gcodeLines[gcodePosition];
            Debug.Log("[Gcode] " + currentGcodeLine);

            GcodeLine gcodeLine = gcodeParser.ParseLine(currentGcodeLine);
            if (gcodeLine != null) {
                switch (gcodeLine.command) {
                    case MarlinGcode.G0_LINEAR_MOVE_NON_EXTRUSION:
                        if (gcodeLine.HasParameter('X')) {
                            xAxis.MoveTo(gcodeLine.GetFloatParameter('X'), 500f);
                        }

                        if (gcodeLine.HasParameter('Y')) {
                            yAxis.MoveTo(gcodeLine.GetFloatParameter('Y'), 500f);
                        }

                        if (gcodeLine.HasParameter('Z')) {
                            zAxis.MoveTo(gcodeLine.GetFloatParameter('Z'), 500f);
                        }

                        trail.emitting = false;

                        break;
                    case MarlinGcode.G1_LINEAR_MOVE_EXTRUSION:
                        if (gcodeLine.HasParameter('X')) {
                            xAxis.MoveTo(gcodeLine.GetFloatParameter('X'), 500f);
                        }

                        if (gcodeLine.HasParameter('Y')) {
                            yAxis.MoveTo(gcodeLine.GetFloatParameter('Y'), 500f);
                        }

                        if (gcodeLine.HasParameter('Z')) {
                            zAxis.MoveTo(gcodeLine.GetFloatParameter('Z'), 500f);
                        }

                        trail.emitting = true;

                        break;
                    default:
                        break;
                }
            }
        }
    }
}

public enum MarlinGcode {
    G0_LINEAR_MOVE_NON_EXTRUSION,
    G1_LINEAR_MOVE_EXTRUSION
}

public class GcodeParser {
    private Dictionary<string, MarlinGcode> stringToCode = new Dictionary<string, MarlinGcode>() {
        ["G0"] = MarlinGcode.G0_LINEAR_MOVE_NON_EXTRUSION,
        ["G1"] = MarlinGcode.G1_LINEAR_MOVE_EXTRUSION
    };

    public GcodeLine ParseLine(string line) {
        line = line.Trim();

        if (line.StartsWith(';')) {
            return null;
        }

        string[] components = line.Split(' ');

        if (components.Length == 0) {
            return null;
        }

        string commandString = components[0];
        if (!stringToCode.ContainsKey(commandString)) {
            return null;
        }

        GcodeLine gcodeLine = new GcodeLine();
        gcodeLine.command = stringToCode[commandString];

        for (int i = 1; i < components.Length; i++) {
            string parameterWithValue = components[i];

            char parameterName = parameterWithValue[0];
            string parameterValue = parameterWithValue.Substring(1);

            gcodeLine.parameters.Add(parameterName, parameterValue);
        }

        return gcodeLine;
    }
}

public class GcodeLine {
    public MarlinGcode command;
    public Dictionary<char, string> parameters;

    public GcodeLine() {
        parameters = new Dictionary<char, string>();
    }

    public bool HasParameter(char name) {
        return parameters.ContainsKey(name);
    }

    public int GetIntParameter(char name) {
        return int.Parse(parameters[name], NumberStyles.Any, CultureInfo.InvariantCulture);
    }

    public float GetFloatParameter(char name) {
        return float.Parse(parameters[name], NumberStyles.Any, CultureInfo.InvariantCulture);
    }
}
