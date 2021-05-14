# KUKA-UDP-Sender
Description

This is some code we used a few years ago to send variables from a KUKA robot via UDP to an external system. It's very barebones and not cleaned up in any way, but should work.

It can access KUKA variables through two DLLs that you need to get in the right version from your robot:

KukaRoboter.Common.XmlResources.dll

KukaRoboter.LegacyKrcServiceLib.dll

Make sure those DLLs are next to the EXE after building it. You could modify to also e.g. send the status of a digital input as well, so that you only save data when the button is pressed. If you are querying the Cartesian position (e.g. $POS_ACT.X, see commented out parts) note that a tool and base need to be set.
You might have to change some NAT settings in the KUKA UI, but do NOT change any network settings (firewall, IP etc.) on the Windows side of the robot, you've got a good chance of breaking things.
