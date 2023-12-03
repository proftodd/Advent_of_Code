import { findCalibrationValue, sumCalibrationValues } from './calibration'
import { findCalibrationValue as findExpandedCalibrationValue } from './expandedCalibration'

export default function process(data: string[]): void {
    const calibrationValues = data.map(findCalibrationValue)
    const calibrationSum = sumCalibrationValues(calibrationValues)
    console.log(`Sum of calibration values = ${calibrationSum}`)
    const expandedCalibrationValues = data.map(findExpandedCalibrationValue)
    const expandedCalibrationSum = sumCalibrationValues(expandedCalibrationValues)
    console.log(`Sum of expanded calibration values = ${expandedCalibrationSum}`)
}