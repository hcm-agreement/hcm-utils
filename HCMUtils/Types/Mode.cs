namespace HCMUtils.Types;

// NOTE: the definition here differs from the calculation mode definition.
// In the original definition the mode has a side-effect on other
// parameters, e.g. a calculation mode of 10 sets the time parameter to 50%.
public enum Mode
{
    PointToPoint,
    PointToLine
}