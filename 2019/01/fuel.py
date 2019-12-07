file = 'input.txt'
total = 0
with open(file) as f:
    mass = f.readline()
    while mass:
        mass = mass.strip()
        fuel = int(mass) // 3 - 2
        extra_fuel = 0
        fuel_increment = fuel // 3 - 2
        while fuel_increment > 0:
            extra_fuel = extra_fuel + fuel_increment
            fuel_increment = fuel_increment // 3 - 2
        total = total + fuel + extra_fuel
        print(f"{mass} -> {fuel}, {extra_fuel} ({total})")
        mass = f.readline()
print(total)