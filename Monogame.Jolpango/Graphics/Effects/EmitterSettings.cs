using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Effects
{
    public static class EmitterSettings
    {
        public static string FireOrangeJson = """
                {
                "radius": {
                "min": 1,
                "max": 10
                },
                "dispersionMethod": {
                "type": "DispersionInverseCone",
                "direction": {
                    "x": 0,
                    "y": -1
                },
                "length": 100,
                "radius": 20
                },
                "texture": "particle",
                "timeToLive": 1,
                "easing": "EaseInCubic",
                "transitions": [
                {
                    "type": "ColorTransition",
                    "colors": [
                    {
                        "colorName": "Orange"
                    },
                    {
                        "colorName": "DarkOrange"
                    },
                    {
                        "colorName": "Orange"
                    },
                    {
                        "colorName": "Gray"
                    }
                    ]
                },
                {
                    "type": "AlphaTransition",
                    "alphas": [
                    0.5,
                    1,
                    0.8,
                    0.6,
                    0.4,
                    0.2,
                    0
                    ]
                },
                {
                    "type": "ScaleTransition",
                    "scales": [
                    1.5,
                    2.5,
                    2.0,
                    1.75,
                    1.5
                    ]
                }
                ]
            }
            
            """;
        public static string FireCenterJson = """
              {
              "radius": {
                "min": 1,
                "max": 5
              },
              "dispersionMethod": {
                "type": "DispersionInverseCone",
                "direction": {
                  "x": 0,
                  "y": -1
                },
                "length": 100,
                "radius": 20
              },
              "timeToLive": 1,
              "texture": "particle",
              "easing": "EaseInCubic",
              "transitions": [
                {
                  "type": "ColorTransition",
                  "colors": [
                    {
                      "r": 255,
                      "g": 230,
                      "b": 230
                    },
                    {
                      "colorName": "Orange"
                    }
                  ]
                },
                {
                  "type": "AlphaTransition",
                  "alphas": [
                    0.5,
                    1,
                    0.8,
                    0.6,
                    0.4,
                    0.2,
                    0
                  ]
                },
                {
                  "type": "ScaleTransition",
                  "scales": [
                    1.0,
                    2.0,
                    1.8,
                    1.4,
                    1.0
                  ]
                }
              ]
            }
            """;
        public static string SmokeJson = """
              {
              "radius": {
                "min": 1,
                "max": 40
              },
              "dispersionMethod": {
                "type": "DispersionInverseCone",
                "direction": {
                  "x": 0,
                  "y": -1
                },
                "length": 150,
                "radius": 20
              },
              "timeToLive": 2,
              "texture": "smokeparticle",
              "easing": "EaseInCubic",
              "transitions": [
                {
                  "type": "ColorTransition",
                  "colors": [
                    {
                      "colorName": "Gray"
                    },
                    {
                      "colorName": "Gray"
                    }
                  ]
                },
                {
                  "type": "AlphaTransition",
                  "alphas": [
                    0,
                    0.1,
                    0.4,
                    0.1,
                    0,
                  ]
                },
                {
                  "type": "ScaleTransition",
                  "scales": [
                    0,
                    0.5,
                    1,
                    3
                  ]
                },
                {
                  "type": "RotationTransition",
                  "rotation": {
                    "start": 0,
                    "end": 2
                  }
                }
              ]
            }
            
            """;
        public static string FireRedJson = """
                        {
              "radius": {
                "min": 1,
                "max": 10
              },
              "dispersionMethod": {
                "type": "DispersionInverseCone",
                "direction": {
                  "x": 0,
                  "y": -1
                },
                "length": 100,
                "radius": 20
              },
              "texture": "particle",
              "timeToLive": 1,
              "easing": "EaseInCubic",
              "transitions": [
                {
                  "type": "ColorTransition",
                  "colors": [
                    {
                      "colorName": "Red"
                    },
                    {
                      "colorName": "Orange"
                    }
                  ]
                },
                {
                  "type": "AlphaTransition",
                  "alphas": [
                    0.5,
                    1,
                    0.8,
                    0.6,
                    0.4,
                    0.2,
                    0
                  ]
                },
                {
                  "type": "ScaleTransition",
                  "scales": [
                    1.8,
                    2.8,
                    1.8,
                    1.6,
                    1.4
                  ]
                }
              ]
            }
            
            """;
    }
}
