#include "FuncParser/Constant.h"
#include "FuncParser/Math.h"

namespace FuncParserNative
{

Constant::Constant (const std::string & Name, double Value)
{
	m_Name = Name;
	m_Value = Value;
}

const std::string & Constant::GetName () const
{
    return m_Name;
}

const double Constant::GetValue () const
{
    return m_Value;
}

}//.. end "namespace FuncParserNative"
