using System.Collections;
using System.Collections.Generic;
using UI.Enums;
using UnityEngine;

namespace UI.Party
{
    /// <summary>
    /// Message object that is put into a FormattedMessage that party controller outputs
    /// </summary>
    public class PartyControllerMessageObject
    {
        //Requested dropdown type
        public DropdownTypes dropdownRequest = DropdownTypes.Empty;

        public PartyControllerMessageObject(DropdownTypes _dropdownRequest)
        {
            dropdownRequest = _dropdownRequest;
        }
    }
}
