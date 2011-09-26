'iCopy - Simple Photocopier
'Copyright (C) 2007-2011 Matteo Rossi

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.

Public Enum WIA_ERRORS
    WIA_ERROR_UNSPECIFIED_ERROR = &H80210000
    WIA_ERROR_GENERAL_ERROR = &H80210001
    WIA_ERROR_PAPER_JAM = &H80210002
    WIA_ERROR_PAPER_EMPTY = &H80210003
    WIA_ERROR_PAPER_PROBLEM = &H80210004
    WIA_ERROR_OFFLINE = &H80210005
    WIA_ERROR_BUSY = &H80210006
    WIA_ERROR_WARMING_UP = &H80210007
    WIA_ERROR_USER_INTERVENTION = &H80210008
    WIA_ERROR_ITEM_DELETED = &H80210009
    WIA_ERROR_DEVICE_COMMUNICATION = &H8021000A
    WIA_ERROR_INVALID_COMMAND = &H8021000B
    WIA_ERROR_INCORRECT_HARDWARE_SETTING = &H8021000C
    WIA_ERROR_DEVICE_LOCKED = &H8021000D
    WIA_ERROR_EXCEPTION_IN_DRIVER = &H8021000E
    WIA_ERROR_INVALID_DRIVER_RESPONSE = &H8021000F
    WIA_ERROR_NOT_REGISTERED = -2147221164
    WIA_ERROR_NO_SCANNER_CONNECTED = &H80210015
    WIA_ERROR_NO_SCANNER_SELECTED = &H80210064
    WIA_ERROR_CONNECTION_ERROR = &H80070077
    WIA_ERROR_UNKNOWN_ERROR = &H80210067
End Enum

Enum WIA_PROPERTIES
    WIA_RESERVED_FOR_NEW_PROPS = 1024
    WIA_DIP_FIRST = 2
    WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS
    WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS

    ' Scanner only device properties (DPS)
    WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS
    WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13
    WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14
End Enum

Enum WIA_DPS_DOCUMENT_HANDLING_STATUS
    FEED_READY = &H1
End Enum

Enum WIA_DPS_DOCUMENT_HANDLING_SELECT
    FEEDER = &H1
    FLATBED = &H2
End Enum
