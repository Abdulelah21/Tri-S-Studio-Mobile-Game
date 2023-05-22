Thank you for purchasing SensorToolkit 2!

The documentation is available online at: http://www.micosmo.com/sensortoolkit2
There are some example scenes inside Examples/ directory. I recommend first viewing the 'Fundamentals' scene.

If you have any questions, feature requests or if you have found a bug then please send me an email at micosmogames@gmail.com


[CHANGELOG]
2.3.2 - New LOSColliderOwner component can instruct LOSSensor which colliders to ignore when testing an object for LOS
2.3.1 - Replaced FOVCollider.BaseSize with 'NearDistance'. Made widget colours configurable. Fix a bug where RaySensor.Clear() won't reset 'isObstructed'.
2.3.0 - Added integration for Game Creator 2
2.2.11 - Removed shaders causing compilation errors for some users
2.2.10 - Sensor has new events 'OnSomeDetection' and 'OnNoDetection'
2.2.9 - Sensor can be Cleared. Sensor.PulseAll will also pulse any input sensors.
2.2.8 - Simplified and improved the built-in locomotion used by the Steering Sensor.
2.2.7 - Small fix to previous update.
2.2.6 - PlayMaker actions will take either a GameObject owner or specific Sensor to target.
2.2.5 -  New 'Sensor' type in PlayMaker variable-type dropdown. PlayMaker actions all take 'BasePulsableSensor'.
2.2.4 - Small improvement to Observable<T> class.
2.2.3 - Small tweaks to filtering functions. Slightly improved performance.
2.2.2 - LOSSensor has new prop 'PointGenerationMethod', which can be 'fast' or 'quality'. The 'quality' mode will restrict the test points to the fov, 'fast' will not.
2.2.1 - Small fix for LOSSensor. It should detect a signal when it's inside its bounds.
2.2.0 - Big improvements to LOSSensor. It will now generate test points within its defined angle constraints.
2.1.3 - Sensor pulses can now optionally be run in FixedUpdate. Fixed issue causing sensor pulses not to be staggered.
2.1.2 - Small bugfix. Signal.Bounds no longer throws NRE when Signal.Object is null.
2.1.1 - Small bugfixes for Playmaker actions.
2.1.0 - Added integration for Behavior Designer.
2.0.3 - Bugfix for LOSSensor so it will generate proper test points on rotated objects.
2.0.2 - Removed a List.AddRange which had slipped through and caused GC.
2.0.1 - No functional changes. Improved formatting and comments of all the sensors code.