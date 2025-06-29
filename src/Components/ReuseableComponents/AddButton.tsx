import React from 'react'
import Button from './Button'
import { MdAddCircle } from 'react-icons/md'

export default function AddButton({ label, reset }: { label: string }) {
    return <Button className="flex flex-row justify-between  gap-3 text-xl" roundedMd success hover  {...reset}> {label}<span className="text-white shadow rounded-full border border-2 border-green-200"><MdAddCircle className="text-xl" /> </span></Button>

}
