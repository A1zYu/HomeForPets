namespace HomeForPets.Models;

public class Volunteer
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<Pet> _pets=[];
    private readonly List<SocialNetwork> _socialNetworks=[];
    
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int YearsOfExperience { get; private set; }
    public int PetHomeFoundCount { get; private set; }
    public int PetSearchForHomeCount { get; private set; }
    public int PetTreatmentCount { get; private set; }
    public int PhoneNumber { get; private set; }
    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;
    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<SocialNetwork> SocialNetworks=> _socialNetworks;
}

#region Описание волонтера
// Нужно создать модель волонтёра - Volunteer со следующими полями:
//
// 1. Id
// 2. ФИО
// 3. Общее описание
// 4. Опыт в годах
// 5. Количество домашних животных, которые смогли найти дом
// 6. Количество домашних животных, которые ищут дом в данный момент времени
// 7. Количество животных, которые сейчас находятся на лечении
// 8. Номер телефона
// 9. Социальные сети (список соц. сетей, у каждой социальной сети должна быть ссылка и название), поэтому нужно сделать отдельный класс для социальной сети.
// 10. Реквизиты для помощи (у каждого реквизита будет название и описание, как сделать перевод), поэтому нужно сделать отдельный класс для реквизита
// 11. Список домашних животных, которыми владеет волонтёр
#endregion
