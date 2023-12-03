import { findCalibrationValue, sumCalibrationValues } from './calibration'
export default function process(data: string[]): void {
    const calibrationValues = data.map(findCalibrationValue)
    const calibrationSum = sumCalibrationValues(calibrationValues)
    console.log(`Sum of calibration values = ${calibrationSum}`)
}