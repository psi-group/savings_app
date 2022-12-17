import React from 'react'

const OrderSortButton = () => {
  return (
    <select className='w-[150px] rounded-xl bg-slate-200 p-1 outline-none'>
        <option>Sort by: Default</option>
        <option>Sort by: Ordered Items </option>
        <option>Sort by: Order date </option>
        <option>Sort by: Price </option>
    </select>
  )
}

export default OrderSortButton;